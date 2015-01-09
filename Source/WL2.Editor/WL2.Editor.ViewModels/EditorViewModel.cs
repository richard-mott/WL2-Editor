using System;
using System.Collections.Generic;
using System.Linq;
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

        public event Action<object, RequestSaveFileEventArgs> RequestSaveFileEvent;

        public IEnumerable<CharacterViewModel> Characters
        {
            get { return _characters.Select(character => new CharacterViewModel(character)); }
        }

        public ICommand OpenSaveFile
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
                            LoadSaveFile(requestArgs.FileName);
                        }
                    });
            }
        }

        private void LoadSaveFile(string fileName)
        {
            var doc = XDocument.Load(fileName);

            var characters = doc.Descendants("PcData");

            foreach (var characterData in characters)
            {
                _characters.Add(new Character(characterData));
            }
        }
    }
}
