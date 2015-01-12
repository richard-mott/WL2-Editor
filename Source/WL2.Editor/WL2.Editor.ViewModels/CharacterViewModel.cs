using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Assisticant;
using WL2.Editor.Models;

namespace WL2.Editor.ViewModels
{
    public class CharacterViewModel
    {
        private const int AttributeMinimum = 1;
        private const int AttributeMaximum = 10;
        private const int SkillMinimum = 0;
        private const int SkillMaximum = 10;

        private readonly Character _character;

        public CharacterViewModel(Character character)
        {
            _character = character;
        }

        public string Name
        {
            get { return _character.Name; }
        }

        public int InitialAttributePoints
        {
            get { return _character.InitialAttributePoints; }
        }

        public int CurrentAttributePoints
        {
            get { return _character.CurrentAttributePoints; }
        }

        public int InitialSkillPoints
        {
            get { return _character.InitialSkillPoints; }
        }

        public int CurrentSkillPoints
        {
            get { return _character.CurrentSkillPoints; }
        }

        public IEnumerable<StatisticViewModel> Attributes
        {
            get
            {
                return _character.Attributes
                    .Select(attribute => new StatisticViewModel(attribute, AttributeMinimum, AttributeMaximum));
            }
        }

        public IEnumerable<StatisticViewModel> WeaponSkills
        {
            get
            {
                return _character.Skills
                    .Where(skill => skill.Category == SkillCategory.Weapon)
                    .OrderBy(skill => skill.Name)
                    .Select(skill => new StatisticViewModel(skill, SkillMinimum, SkillMaximum));
            }
        }

        public IEnumerable<StatisticViewModel> GeneralSkills
        {
            get
            {
                return _character.Skills
                    .Where(skill => skill.Category == SkillCategory.General)
                    .OrderBy(skill => skill.Name)
                    .Select(skill => new StatisticViewModel(skill, SkillMinimum, SkillMaximum));
            }
        }

        public IEnumerable<StatisticViewModel> KnowledgeSkills
        {
            get
            {
                return _character.Skills
                    .Where(skill => skill.Category == SkillCategory.Knowledge)
                    .OrderBy(skill => skill.Name)
                    .Select(skill => new StatisticViewModel(skill, SkillMinimum, SkillMaximum));
            }
        }

        public IEnumerable<StatisticViewModel> MiscellaneousSkills
        {
            get
            {
                return _character.Skills
                    .Where(skill => skill.Category == SkillCategory.Miscellaneous)
                    .OrderBy(skill => skill.Name)
                    .Select(skill => new StatisticViewModel(skill, SkillMinimum, SkillMaximum));
            }
        }

        public ICommand ResetAttributes
        {
            get
            {
                return MakeCommand
                    .Do(() => _character.ResetAttributes());
            }
        }

        public ICommand RestoreAttributes
        {
            get
            {
                return MakeCommand
                    .Do(() => _character.RestoreAttributes());
            }
        }

        public ICommand ResetSkills
        {
            get
            {
                return MakeCommand
                    .Do(() => _character.ResetSkills());
            }
        }

        public ICommand RestoreSkills
        {
            get
            {
                return MakeCommand
                    .Do(() => _character.RestoreSkills());
            }
        }
    }
}
