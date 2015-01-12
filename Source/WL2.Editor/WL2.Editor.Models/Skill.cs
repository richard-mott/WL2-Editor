using System;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using Assisticant.Fields;

namespace WL2.Editor.Models
{
    public class Skill : IStatistic
    {
        public Action<object, StatisticChangedEventArgs> SkillChanged;
 
        private readonly Observable<int> _currentValue = new Observable<int>(0);
        private readonly int _initialValue;
        private readonly XElement _valueElement;
        
        public string Name { get; private set; }
        public string Image { get; private set; }
        public SkillCategory Category {get; private set;}

        public int CurrentValue
        {
            get { return _currentValue.Value; }
            set
            {
                if (_currentValue.Value != value)
                {
                    RaiseStatisticChanged(_currentValue.Value, value);
                    _currentValue.Value = value;
                }
            }
        }

        public bool IsDirty
        {
            get { return CurrentValue != _initialValue; }
        }

        public Skill(XElement skillData)
        {
            _valueElement = GetValueElement(skillData);

            Name = ParseName(skillData);
            Image = ParseImage(skillData);
            Category = ParseCategory(skillData);

            _initialValue = ParseValue(skillData);
            CurrentValue = _initialValue;
        }

        public void Save()
        {
            var saveValue = Statistics.SkillSaveValues[CurrentValue];

            _valueElement.Value = saveValue.ToString(CultureInfo.InvariantCulture);
        }

        public void Reset()
        {
            CurrentValue = 0;
        }

        public void Restore()
        {
            CurrentValue = _initialValue;
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

        private string ParseImage(XElement skillData)
        {
            var nameElement = skillData.Element("Key");

            if (nameElement == null)
                throw new XmlException("Attribute data requires a Key element.");

            return "skill_" + nameElement.Value + ".png";
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

            return Statistics.SkillDisplayValues[value];
        }
        
        private void RaiseStatisticChanged(int oldValue, int newValue)
        {
            if (SkillChanged != null)
            {
                SkillChanged(this, new StatisticChangedEventArgs(oldValue, newValue));
            }
        }
    }
}
