using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Windows.Input;
using System.Xml.Linq;
using Assisticant;
using Assisticant.Collections;
using WL2.Editor.Models;

namespace WL2.Editor.ViewModels
{
    public class EditorViewModel
    {
        private readonly ObservableList<Character> _characters = new ObservableList<Character>();
        private XDocument _document;
        private string _fileName;

        public event Action<object, RequestSaveFileEventArgs> RequestSaveFileEvent;

        public IEnumerable<CharacterViewModel> Characters
        {
            get { return _characters.Select(character => new CharacterViewModel(character)); }
        }

        public ICommand OpenFile
        {
            get
            {
                return MakeCommand
                    .Do(() =>
                    {
                        var requestArgs = new RequestSaveFileEventArgs();

                        if (RequestSaveFileEvent != null)
                        {
                            RequestSaveFileEvent(this, requestArgs);
                        }

                        if (requestArgs.IsConfirmed)
                        {
                            LoadFile(requestArgs.FileName);
                        }
                    });
            }
        }

        public ICommand SaveFile
        {
            get
            {
                return MakeCommand
                    .When(() => IsFileDirty)
                    .Do(SaveDocument);
            }
        }

        private bool IsFileDirty
        {
            get { return _characters.Any(character => character.IsDirty); }
        }

        private void LoadFile(string fileName)
        {
            _document = XDocument.Load(fileName);
            _fileName = fileName;

            var characters = _document.Descendants("PcData");

            foreach (var characterData in characters)
            {
                _characters.Add(new Character(characterData));
            }
        }

        private void SaveDocument()
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
