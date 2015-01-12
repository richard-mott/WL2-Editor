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
        private readonly SaveGame _saveGame;

        public event Action<object, RequestSaveFileEventArgs> RequestSaveFileEvent;

        public EditorViewModel(SaveGame saveGame)
        {
            _saveGame = saveGame;
        }

        public IEnumerable<CharacterViewModel> Characters
        {
            get
            {
                return _saveGame.Characters
                    .Select(character => new CharacterViewModel(character));
            }
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
                            _saveGame.Load(requestArgs.FileName);
                        }
                    });
            }
        }

        public ICommand SaveFile
        {
            get
            {
                return MakeCommand
                    .When(() => _saveGame.IsDirty)
                    .Do(() => _saveGame.Save());
            }
        }
    }
}
