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
using System.Windows.Shapes;

namespace WL2.Editor.Views
{
    /// <summary>
    /// Interaction logic for WL2MessageBox.xaml
    /// </summary>
    public partial class WL2MessageBox : Window
    {
        public WL2MessageBox()
        {
            InitializeComponent();
        }

        private void On_OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
