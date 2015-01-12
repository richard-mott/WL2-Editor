using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Assisticant;
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
                    .Do(Open);
            }
        }

        public ICommand SaveFile
        {
            get
            {
                return MakeCommand
                    .When(IsDirty)
                    .Do(Save);
            }
        }

        public ICommand ResetAllAttributes
        {
            get
            {
                return MakeCommand
                    .Do(() => _saveGame.ResetAllAttributes());
            }
        }

        public ICommand RestoreAllAttributes
        {
            get
            {
                return MakeCommand
                    .Do(() => _saveGame.RestoreAllAttributes());
            }
        }

        public ICommand ResetAllSkills
        {
            get
            {
                return MakeCommand
                    .Do(() => _saveGame.ResetAllSkills());
            }
        }

        public ICommand RestoreAllSkills
        {
            get
            {
                return MakeCommand
                    .Do(() => _saveGame.RestoreAllSkills());
            }
        }

        private void Open()
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
        }

        private bool IsDirty()
        {
            return _saveGame.IsDirty;
        }

        private void Save()
        {
            _saveGame.Save();
        }
    }
}
