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

        public SaveGame()
        {}

        public SaveGame(XDocument saveData)
        {
            _document = saveData;

            var characters = _document.Descendants("PcData");

            foreach (var characterData in characters)
            {
                _characters.Add(new Character(characterData));
            }
        }

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
            _characters.Clear();

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

        public void ResetAllAttributes()
        {
            foreach (var character in _characters)
            {
                character.ResetAttributes();
            }
        }

        public void RestoreAllAttributes()
        {
            foreach (var character in _characters)
            {
                character.RestoreAttributes();
            }
        }

        public void ResetAllSkills()
        {
            foreach (var character in _characters)
            {
                character.ResetSkills();
            }
        }

        public void RestoreAllSkills()
        {
            foreach (var character in _characters)
            {
                character.RestoreSkills();
            }
        }
    }
}
