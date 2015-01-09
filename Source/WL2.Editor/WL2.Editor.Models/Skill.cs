using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using Assisticant.Fields;

namespace WL2.Editor.Models
{
    public class Skill : IStatistic
    {
        private readonly Observable<int> _currentValue = new Observable<int>(0);
        private readonly XElement _valueElement;
        
        public string Name { get; private set; }
        public SkillCategory Category {get; private set;}

        public int CurrentValue
        {
            get { return _currentValue.Value; }
            set { _currentValue.Value = value; }
        }

        public Skill(XElement skillData)
        {
            _valueElement = GetValueElement(skillData);

            Name = ParseName(skillData);
            Category = ParseCategory(skillData);

            var parsedValue = ParseValue(skillData);

            if (parsedValue == -1)
            {
                throw new XmlException("Invalid value for skill.");
            }
            
            CurrentValue = Statistics.SkillDisplayValues[parsedValue];
        }

        public void Save()
        {
            var saveValue = Statistics.SkillSaveValues[CurrentValue];

            _valueElement.Value = saveValue.ToString(CultureInfo.InvariantCulture);
        }


        private XElement GetValueElement(XElement skillData)
        {
            var valueElement = skillData.Element("Value");

            if (valueElement == null)
                throw new XmlException("Attribute data requires a Value element.");

            return valueElement;
        }

        private string ParseName(XElement skillData)
        {
            var nameElement = skillData.Element("Key");

            if (nameElement == null)
                throw new XmlException("Attribute data requires a Key element.");

            return Statistics.SkillNames[nameElement.Value];
        }

        private SkillCategory ParseCategory(XElement skillData)
        {
            var nameElement = skillData.Element("Key");

            if (nameElement == null)
                throw new XmlException("Attribute data requires a Key element.");

            return Statistics.SkillCategories[nameElement.Value];
        }

        private int ParseValue(XElement skillData)
        {
            var valueElement = GetValueElement(skillData);
            int value;

            if (!int.TryParse(valueElement.Value, out value))
            {
                throw new XmlException("Invalid value for skill.");
            }

            return value;
        }
    }
}
