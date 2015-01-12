using System.Linq;
using System.Xml.Linq;
using Assisticant;
using WL2.Editor.Models;

namespace WL2.Editor.ViewModels
{
    public class ViewModelLocator : ViewModelLocatorBase
    {
        private readonly SaveGame _saveGame;

        public ViewModelLocator()
        {
            _saveGame = DesignMode 
                ? new SaveGame(LoadSampleData()) 
                : new SaveGame();
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
                        new Attribute("Intelligence", "attribute_intelligence.png", 6), 
                        1, 10));
            }
        }

        public object CharacterViewModel
        {
            get
            {
                return (DesignMode)
                    ? new CharacterViewModel(_saveGame.Characters.First())
                    : null;
            }
        }

        private XDocument LoadSampleData()
        {
            return XDocument.Parse(Properties.Resources.SampleData);
        }
    }
}
