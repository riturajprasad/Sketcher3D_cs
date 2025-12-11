using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GeometryEngine3D;

namespace Sketcher3D
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double x = double.Parse(txtX.Text);
                double y = double.Parse(txtY.Text);
                double z = double.Parse(txtZ.Text);

                GeometryEngine3D.Point pt = new GeometryEngine3D.Point(x, y, z);

                txtOutput.Text =
                $"X = {pt.getX()}\n" +
                $"Y = {pt.getY()}\n" +
                $"Z = {pt.getZ()}";
            }
            catch
            {
                MessageBox.Show("Please enter valid numeric values for X, Y, Z.");
            }
        }

        private void txtX_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtY_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtZ_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtOutput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
