using Assisticant;
using WL2.Editor.Models;

namespace WL2.Editor.ViewModels
{
    public class ViewModelLocator : ViewModelLocatorBase
    {
        private readonly SaveGame _saveGame;

        public ViewModelLocator()
        {
            _saveGame = new SaveGame();
        }

        public object EditorViewModel
        {
            get { return ViewModel(() => new EditorViewModel(_saveGame)); }
        }

        public object AttributeViewModel
        {
            get
            {
                return ViewModel(() =>
                    new StatisticViewModel(
                        new Attribute("Intelligence", "attribute_intelligence", 6), 
                        1, 10));
            }
        }
    }
}
