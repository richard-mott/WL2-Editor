using System.Globalization;
using System.Xml;
using System.Xml.Linq;

namespace WL2.Editor.Models
{
    public class AvailablePoints : IStatistic
    {
        private readonly int _initialValue;
        private readonly XElement _pointsElement;

        public string Name { get; private set; }
        public string Image { get; private set; }
        public int CurrentValue { get; set; }

        public bool IsDirty
        {
            get { return CurrentValue != _initialValue; }
        }

        public AvailablePoints(XElement availablePointsData)
        {
            _pointsElement = availablePointsData;

            Name = availablePointsData.Name.ToString();
            Image = string.Empty;

            _initialValue = ParseValue(availablePointsData);
            CurrentValue = _initialValue;
        }

        public void Save()
        {
            _pointsElement.Value = CurrentValue.ToString(CultureInfo.InvariantCulture);
        }

        private int ParseValue(XElement availablePointsData)
        {
            int value;

            if (!int.TryParse(availablePointsData.Value, out value))
            {
                throw new XmlException("Invalid value for skill.");
            }

            return value;
        }
    }
}
