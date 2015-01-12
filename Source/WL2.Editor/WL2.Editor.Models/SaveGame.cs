using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Assisticant.Collections;

namespace WL2.Editor.Models
{
    public class SaveGame
    {
        private readonly ObservableList<Character> _characters = new ObservableList<Character>();
        private string _fileName;
        private XDocument _document;

        public IEnumerable<Character> Characters
        {
            get { return _characters; }
        }

        public bool IsDirty
        {
            get { return _characters.Any(character => character.IsDirty); }
        }

        public void Load(string fileName)
        {
            _fileName = fileName;
            _document = XDocument.Load(fileName);

            var characters = _document.Descendants("PcData");

            foreach (var characterData in characters)
            {
                _characters.Add(new Character(characterData));
            }
        }

        public void Save()
        {
            var dirtyCharacters = _characters.Where(character => character.IsDirty);

            foreach (var character in dirtyCharacters)
            {
                character.Save();
            }

            _document.Save(_fileName);
        }
    }
}
