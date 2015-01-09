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
using Assisticant;
using Microsoft.Win32;
using WL2.Editor.ViewModels;

namespace WL2.Editor.Views
{
    /// <summary>
    /// Interaction logic for EditorView.xaml
    /// </summary>
    public partial class EditorView : UserControl
    {
        public EditorView()
        {
            InitializeComponent();
        }

        private EditorViewModel ViewModel
        {
            get { return ForView.Unwrap<EditorViewModel>(DataContext); }
        }

        private void EditorView_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.RequestSaveFileEvent += RequestSaveFileEventHandler;
        }

        private void EditorView_Unloaded(object sender, RoutedEventArgs e)
        {
            ViewModel.RequestSaveFileEvent -= RequestSaveFileEventHandler;
        }

        private void RequestSaveFileEventHandler(object sender, RequestSaveFileEventArgs e)
        {
            var openDialog = new OpenFileDialog();

            openDialog.DefaultExt = ".xml";
            openDialog.Filter = "Wasteland 2 Save Files (*.xml)|*.xml";
            openDialog.Multiselect = false;

            if (openDialog.ShowDialog() == true)
            {
                e.FileName = openDialog.FileName;
                e.IsConfirmed = true;
            }
            else
            {
                e.IsConfirmed = false;
            }
        }
    }
}
