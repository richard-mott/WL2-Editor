using Assisticant;

namespace WL2.Editor.ViewModels
{
    public class ViewModelLocator : ViewModelLocatorBase
    {
        public ViewModelLocator()
        {
            
        }

        public object EditorViewModel
        {
            get { return ViewModel(() => new EditorViewModel()); }
        }
    }
}
