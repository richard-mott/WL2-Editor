using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace WL2.Editor.Models
{
    public class Character
    {
        private readonly List<Attribute> _attributes = new List<Attribute>();

        private readonly Dictionary<SkillCategory, IList<Skill>> _skills = 
            new Dictionary<SkillCategory, IList<Skill>>
        {
            {SkillCategory.Weapon, new List<Skill>()},
            {SkillCategory.General, new List<Skill>()},
            {SkillCategory.Knowledge, new List<Skill>()},
            {SkillCategory.Miscellaneous, new List<Skill>()}
        };

        public string Name { get; private set; }

        public Dictionary<SkillCategory, IList<Skill>> Skills
        {
            get { return _skills; }
        }

        public IEnumerable<Attribute> Attributes
        {
            get { return _attributes; }
        }

        public Character(XElement characterData)
        {
            Name = ParseName(characterData);
            ParseAttributes(characterData);
            ParseSkills(characterData);
        }

        private string ParseName(XElement characterData)
        {
            var nameValue = characterData.Element("displayName");

            if (nameValue == null)
            {
                throw new XmlException("Invalid name value.");
            }

            var name = nameValue.Value;

            if (name.StartsWith("<@>"))
            {
                name = name.Remove(0, 3);
            }

            if (name.EndsWith("}"))
            {
                name = name.Remove(name.Length - 3);
            }

            return name;
        }

        private void ParseAttributes(XElement characterData)
        {
            var attributesElement = characterData.Element("attributes2");

            if (attributesElement == null)
            {
                throw new XmlException("Could not find attribute data.");
            }

            var attributesList = attributesElement.Descendants("KeyValuePairOfStringInt32");

            foreach (var attributeData in attributesList)
            {
                _attributes.Add(new Attribute(attributeData));
            }
        }

        private void ParseSkills(XElement characterData)
        {
            var skillsElement = characterData.Element("skillXps2");

            if (skillsElement == null)
            {
                throw new XmlException("Could not find skills data.");
            }

            var skillsList = skillsElement.Descendants("KeyValuePairOfStringInt32");

            foreach (var skillData in skillsList)
            {
                var skill = new Skill(skillData);

                _skills[skill.Category].Add(skill);
            }
        }
    }
}
