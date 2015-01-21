using System;
using System.Windows;
using System.Windows.Controls;
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
            ViewModel.RequestSaveFileEvent += OnRequestSaveFile;
            ViewModel.SaveSuccessfulEvent += OnSaveSuccessful;
        }

        private void EditorView_Unloaded(object sender, RoutedEventArgs e)
        {
            ViewModel.RequestSaveFileEvent -= OnRequestSaveFile;
            ViewModel.SaveSuccessfulEvent -= OnSaveSuccessful;
        }

        private void OnRequestSaveFile(object sender, RequestSaveFileEventArgs e)
        {
            var openDialog = new OpenFileDialog
            {
                DefaultExt = ".xml",
                Filter = "Wasteland 2 Save Files (*.xml)|*.xml",
                Multiselect = false
            };

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

        private void OnSaveSuccessful(object sender, EventArgs e)
        {
            var window = Window.GetWindow(this);

            var messageBox = new WL2MessageBox
            {
                Owner = window
            };

            messageBox.ShowDialog();
        }
    }
}
