﻿using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using Assisticant.Fields;

namespace WL2.Editor.Models
{
    public class Attribute : IStatistic
    {
        private readonly Observable<int> _currentValue = new Observable<int>(0);
        private readonly int _initialValue;
        private readonly XElement _valueElement;
        
        public string Name { get; private set; }
        public string Image { get; private set; }

        public int CurrentValue
        {
            get { return _currentValue.Value; }
            set { _currentValue.Value = value; }
        }

        public bool IsDirty
        {
            get { return CurrentValue != _initialValue; }
        }

        public Attribute(string name, string image, int currentValue)
        {
            Name = name;
            Image = image;
            CurrentValue = currentValue;
        }

        public Attribute(XElement attributeData)
        {
            _valueElement = GetValueElement(attributeData);
            
            Name = ParseName(attributeData);
            Image = ParseImage(attributeData);

            _initialValue = ParseValue(attributeData);
            CurrentValue = _initialValue;
        }

        public void Save()
        {
            _valueElement.Value = CurrentValue.ToString(CultureInfo.InvariantCulture);
        }

        private XElement GetValueElement(XElement attributeData)
        {
            var valueElement = attributeData.Element("Value");

            if (valueElement == null)
                throw new XmlException("Attribute data requires a Value element.");

            return valueElement;
        }

        private string ParseName(XElement attributeData)
        {
            var nameElement = attributeData.Element("Key");

            if (nameElement == null)
                throw new XmlException("Attribute data requires a Key element.");

            return Statistics.AttributeNames[nameElement.Value];
        }

        private string ParseImage(XElement attributeData)
        {
            var nameElement = attributeData.Element("Key");

            if (nameElement == null)
                throw new XmlException("Attribute data requires a Key element.");

            return "attribute_" + nameElement.Value + ".png";
        }

        private int ParseValue(XElement attributeData)
        {
            var valueElement = GetValueElement(attributeData);
            int value;

            if (!int.TryParse(valueElement.Value, out value))
            {
                throw new XmlException("Invalid value for attribute.");
            }

            return value;
        }
    }
}
