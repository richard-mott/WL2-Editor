using System.Collections.Generic;
using System.Linq;
using WL2.Editor.Models;

namespace WL2.Editor.ViewModels
{
    public class CharacterViewModel
    {
        private readonly Character _character;

        public CharacterViewModel(Character character)
        {
            _character = character;
        }

        public string Name
        {
            get { return _character.Name; }
        }

        public IEnumerable<StatisticViewModel> Attributes
        {
            get { return _character.Attributes.Select(attribute => new StatisticViewModel(attribute)); }
        }

        public IEnumerable<StatisticViewModel> WeaponSkills
        {
            get
            {
                var weaponSkills = _character.Skills[SkillCategory.Weapon];
                return weaponSkills.Select(skill => new StatisticViewModel(skill));
            }
        }

        public IEnumerable<StatisticViewModel> GeneralSkills
        {
            get
            {
                var generalSkills = _character.Skills[SkillCategory.General];
                return generalSkills.Select(skill => new StatisticViewModel(skill));
            }
        }

        public IEnumerable<StatisticViewModel> KnowledgeSkills
        {
            get
            {
                var knowledgeSkills = _character.Skills[SkillCategory.Knowledge];
                return knowledgeSkills.Select(skill => new StatisticViewModel(skill));
            }
        }

        public IEnumerable<StatisticViewModel> MiscellaneousSkills
        {
            get
            {
                var miscellaneousSkills = _character.Skills[SkillCategory.Miscellaneous];
                return miscellaneousSkills.Select(skill => new StatisticViewModel(skill));
            }
        }
    }
}
