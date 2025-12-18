using GeometryEngine3D;          // Geometry engine (Shape, Triangulation)
using Microsoft.Win32;                  // For Open/Save file dialogs
using System;
using System.Windows;                   // Core WPF window functionality
using System.Windows.Input;             // Mouse and keyboard input handling
using System.Windows.Media;             // Colors, brushes, materials
using System.Windows.Media.Media3D;     // 3D types: Camera, Mesh, Transforms
// Alias to avoid ambiguity between WPF Point and engine Point
using WpfPoint = System.Windows.Point;

namespace Sketcher3D
{
    /// <summary>
    /// Main application window.
    /// Responsible for:
    /// - Rendering 3D geometry
    /// - Handling mouse interaction (rotate, pan, zoom)
    /// - Bridging geometry engine with WPF renderer
    /// </summary>
    public partial class MainWindow : Window
    {
        // Manages all geometry objects (engine level)
        private readonly ShapeManager _shapeManager = new ShapeManager();

        // =====================================================
        // SCENE TRANSFORMS
        // Applied to SceneRoot so all objects move together
        // =====================================================

        private Transform3DGroup _sceneTransform;   // Root transform group
        private AxisAngleRotation3D _rotX;           // Rotation around X axis
        private AxisAngleRotation3D _rotY;           // Rotation around Y axis
        private TranslateTransform3D _pan;           // Pan (move scene in XY plane)

        // =====================================================
        // MOUSE STATE
        // Tracks interaction mode and last mouse position
        // =====================================================

        private WpfPoint _lastPos;                   // Previous mouse position
        private bool _rotating;                      // Left mouse button state
        private bool _panning;                       // Right mouse button state

        /// <summary>
        /// Main window constructor.
        /// Initializes UI and 3D scene transforms.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();   // Load XAML UI
            InitSceneTransforms();   // Setup rotation and pan transforms
        }

        // =====================================================
        // SCENE SETUP
        // =====================================================

        /// <summary>
        /// Initializes scene-level transforms.
        /// All 3D objects are children of SceneRoot,
        /// so these transforms affect the entire scene.
        /// </summary>
        private void InitSceneTransforms()
        {
            // Rotation around X axis (pitch)
            _rotX = new AxisAngleRotation3D(new Vector3D(1, 0, 0), 0);

            // Rotation around Y axis (yaw)
            _rotY = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);

            // Translation for panning the scene
            _pan = new TranslateTransform3D();

            // Combine all transforms into a group
            _sceneTransform = new Transform3DGroup();
            _sceneTransform.Children.Add(new RotateTransform3D(_rotX));
            _sceneTransform.Children.Add(new RotateTransform3D(_rotY));
            _sceneTransform.Children.Add(_pan);

            // Apply transform group to scene root
            SceneRoot.Transform = _sceneTransform;
        }

        // =====================================================
        // SHAPE CREATION & RENDERING
        // =====================================================

        /// <summary>
        /// Converts a geometry-engine Shape into a WPF 3D model
        /// and adds it to the scene.
        /// </summary>
        private void AddShapeToScene(Shape shape, Color color)
        {
            // Store shape in engine-level manager
            _shapeManager.AddShape(shape);

            // Convert triangulated geometry to WPF mesh
            MeshGeometry3D mesh =
                TriangulationMeshBuilder.ToMesh(
                    shape.getTriangulation());

            // Create material using specified color
            DiffuseMaterial material =
                new DiffuseMaterial(new SolidColorBrush(color));

            // Create renderable 3D model
            GeometryModel3D model = new GeometryModel3D(mesh, material)
            {
                // Render both front and back faces
                BackMaterial = material
            };

            // Add model to the scene
            SceneRoot.Children.Add(
                new ModelVisual3D { Content = model });
        }

        // =====================================================
        // MOUSE INTERACTION (ROTATE / PAN / ZOOM)
        // =====================================================

        /// <summary>
        /// Handles mouse button press.
        /// Left button → rotate
        /// Right button → pan
        /// </summary>
        private void View_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _lastPos = e.GetPosition(View);

            if (e.LeftButton == MouseButtonState.Pressed)
                _rotating = true;

            if (e.RightButton == MouseButtonState.Pressed)
                _panning = true;

            // Capture mouse so movement continues outside viewport
            View.CaptureMouse();
        }

        /// <summary>
        /// Handles mouse movement.
        /// Applies rotation or panning based on mouse state.
        /// </summary>
        private void View_MouseMove(object sender, MouseEventArgs e)
        {
            // Ignore if no interaction is active
            if (!_rotating && !_panning)
                return;

            WpfPoint pos = e.GetPosition(View);
            Vector delta = pos - _lastPos;

            // Rotate scene based on mouse movement
            if (_rotating)
            {
                _rotY.Angle += delta.X * 0.5;
                _rotX.Angle += delta.Y * 0.5;
            }

            // Pan scene based on mouse movement
            if (_panning)
            {
                _pan.OffsetX += delta.X * 0.2;
                _pan.OffsetY -= delta.Y * 0.2;
            }

            _lastPos = pos;
        }

        /// <summary>
        /// Handles mouse button release.
        /// Stops interaction.
        /// </summary>
        private void View_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _rotating = false;
            _panning = false;
            View.ReleaseMouseCapture();
        }

        /// <summary>
        /// Handles mouse wheel zoom.
        /// Moves camera closer or farther.
        /// </summary>
        private void View_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double zoom = e.Delta > 0 ? 0.9 : 1.1;

            Camera.Position = new Point3D(
                Camera.Position.X * zoom,
                Camera.Position.Y * zoom,
                Camera.Position.Z * zoom);
        }

        // =====================================================
        // FILE MENU ACTIONS
        // =====================================================

        /// <summary>
        /// Clears scene and resets transformations.
        /// </summary>

        private static void Info(Window owner, string m)
            => MessageBox.Show(owner, m, "Info");
        private void New_Click(object sender, RoutedEventArgs e)
        {
            SceneRoot.Children.Clear();
            InitSceneTransforms();
            _shapeManager.Clear();
        }

        /// <summary>
        /// Clears all shapes from the scene.
        /// </summary>
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            SceneRoot.Children.Clear();
            _shapeManager.Clear();
        }

        /// <summary>
        /// Saves current geometry to a file.
        /// Uses engine-level persistence (not UI objects).
        /// </summary>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog
            {
                Filter = "Text File (*.txt)|*.txt",
                FileName = "scene.txt"
            };

            if (dlg.ShowDialog() == true)
            {
                // Correct CAD approach:
                // Save shapes, not WPF visuals
                FileHandle.WriteSTL(
                    dlg.FileName,
                    _shapeManager.GetShapes());
            }
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // =====================================================
        // SHAPE CREATION BUTTON HANDLERS
        // =====================================================

        private static void Warn(Window owner, string m)
            => MessageBox.Show(owner, m, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        private void Cube_Click(object sender, RoutedEventArgs e)
        {
            double side;
            if (!InputDialogs.AskOne("Cube", "Side", 40, out side)) return;
            try
            {
                Cube s = ShapeCreator.CreateCube("Cube1", side);
                _shapeManager.AddShape(s);
                AddShapeToScene(s, Colors.Orange);
            }
            catch (Exception ex) { Warn(this, ex.Message); }
        }

        private void Cuboid_Click(object sender, RoutedEventArgs e)
        {
            double L, W, H;
            if (!InputDialogs.AskThree("Cuboid", "Length", 50, "Width", 30, "Height", 20, out L, out W, out H)) return;
            try
            {
                var s = ShapeCreator.CreateCuboid("Cuboid1", L, W, H);
                _shapeManager.AddShape(s);
                AddShapeToScene(s, Colors.CornflowerBlue);
            }
            catch (Exception ex) { Warn(this, ex.Message); }
        }
        private void Cylinder_Click(object sender, RoutedEventArgs e)
        {
            double r, h;
            if (!InputDialogs.AskTwo("Cylinder", "Radius", 20, "Height", 50, out r, out h)) return;
            try
            {
                var s = ShapeCreator.CreateCylinder("Cylinder1", r, h);
                _shapeManager.AddShape(s);
                AddShapeToScene(s, Colors.MediumSeaGreen);
            }
            catch (Exception ex) { Warn(this, ex.Message); }
        }
        private void Cone_Click(object sender, RoutedEventArgs e)
        {
            double r, h;
            if (!InputDialogs.AskTwo("Cone", "Radius", 20, "Height", 50, out r, out h)) return;
            try
            {
                var s = ShapeCreator.CreateCone("Cone1", r, h);
                _shapeManager.AddShape(s);
                AddShapeToScene(s, Colors.MediumVioletRed);
            }
            catch (Exception ex) { Warn(this, ex.Message); }
        }
        private void Sphere_Click(object sender, RoutedEventArgs e)
        {
            double r;
            if (!InputDialogs.AskOne("Sphere", "Radius", 25, out r)) return;
            try
            {
                var s = ShapeCreator.CreateSphere("Sphere1", r);
                _shapeManager.AddShape(s);
                AddShapeToScene(s, Colors.Goldenrod);
            }
            catch (Exception ex) { Warn(this, ex.Message); }
        }
        private void Pyramid_Click(object sender, RoutedEventArgs e)
        {
            double L, W, H;
            if (!InputDialogs.AskThree("Pyramid", "Base Length", 40, "Base Width", 40, "Height", 50, out L, out W, out H)) return;
            try
            {
                var s = ShapeCreator.CreatePyramid("Pyramid1", L, W, H);
                _shapeManager.AddShape(s);
                AddShapeToScene(s, Colors.SlateBlue);
            }
            catch (Exception ex) { Warn(this, ex.Message); }
        }
    }
}