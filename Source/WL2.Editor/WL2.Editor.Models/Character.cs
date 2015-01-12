using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Assisticant;
using Assisticant.Fields;

namespace WL2.Editor.Models
{
    public class Character
    {
        private readonly List<Attribute> _attributes = new List<Attribute>();
        private readonly List<Skill> _skills = new List<Skill>();

        private readonly int _initialAttributePoints;
        private readonly int _initialSkillPoints;

        private readonly Observable<int> _currentAttributePoints = new Observable<int>(0);
        private readonly Observable<int> _currentSkillPoints = new Observable<int>(0);

        public string Name { get; private set; }

        public IEnumerable<Attribute> Attributes
        {
            get
            {
                return _attributes
                    .OrderBy(attribute => Statistics.AttributeOrder[attribute.Name]);
            }
        }

        public IEnumerable<Skill> Skills
        {
            get { return _skills; }
        }

        public int InitialAttributePoints
        {
            get { return _initialAttributePoints; }
        }

        public int CurrentAttributePoints
        {
            get { return _currentAttributePoints.Value; }
            set { _currentAttributePoints.Value = value; }
        }

        public int InitialSkillPoints
        {
            get { return _initialSkillPoints; }
        }

        public int CurrentSkillPoints
        {
            get { return _currentSkillPoints.Value; }
            set { _currentSkillPoints.Value = value; }
        }

        public bool IsDirty
        {
            get
            {
                return _attributes.Any(attribute => attribute.IsDirty)
                       || _skills.Any(skill => skill.IsDirty);
            }
        }

        private AvailablePoints AvailableAttributePoints { get; set; }
        private AvailablePoints AvailableSkillPoints { get; set; }

        public Character(XElement characterData)
        {
            Name = ParseName(characterData);
            ParseAttributes(characterData);
            ParseSkills(characterData);

            var availableAttributePointsData = characterData.Element("availableAttributePoints");
            AvailableAttributePoints = new AvailablePoints(availableAttributePointsData);

            var availableSkillPointsData = characterData.Element("availableSkillPoints");
            AvailableSkillPoints = new AvailablePoints(availableSkillPointsData);

            _initialAttributePoints = GetInitialAttributePoints();
            CurrentAttributePoints = _attributes
                .Sum(attribute => attribute.CurrentValue - 1);

            _initialSkillPoints = GetInitialSkillPoints();
            CurrentSkillPoints = _skills
                .Sum(skill => Statistics.SkillSaveValues[skill.CurrentValue]);
        }

        public void Save()
        {
            SaveDirtyStatistics(_attributes);
            SaveDirtyStatistics(_skills);
            SaveAvailableAttributePoints();
            SaveAvailableSkillPoints();
        }

        private void SaveDirtyStatistics(IEnumerable<IStatistic> statistics)
        {
            var dirtyStatistics = statistics.Where(statistic => statistic.IsDirty);

            foreach (var statistic in dirtyStatistics)
            {
                statistic.Save();
            }
        }

        private void SaveAvailableAttributePoints()
        {
            AvailableAttributePoints.CurrentValue = CurrentAttributePoints < InitialAttributePoints
                ? InitialAttributePoints - CurrentAttributePoints
                : 0;

            if (AvailableAttributePoints.IsDirty)
            {
                AvailableAttributePoints.Save();
            }
        }

        private void SaveAvailableSkillPoints()
        {
            AvailableSkillPoints.CurrentValue = CurrentSkillPoints < InitialSkillPoints
                ? InitialSkillPoints - CurrentSkillPoints
                : 0;

            if (AvailableSkillPoints.IsDirty)
            {
                AvailableSkillPoints.Save();
            }
        }

        private string ParseName(XElement characterData)
        {
            var nameElement = characterData.Element("displayName");

            if (nameElement == null)
            {
                throw new XmlException("Invalid name value.");
            }

            var name = nameElement.Value;

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

            foreach (var attribute in attributesList
                .Select(attributeData => new Attribute(attributeData)))
            {
                attribute.AttributeChanged += OnAttributeChanged;
                _attributes.Add(attribute);
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

            foreach (var skill in skillsList
                .Select(skillData => new Skill(skillData)))
            {
                skill.SkillChanged += OnSkillChanged;
                _skills.Add(skill);
            }
        }

        private int GetInitialAttributePoints()
        {
            return AvailableAttributePoints.CurrentValue 
                + _attributes.Sum(attribute => attribute.CurrentValue - 1);
        }

        private int GetInitialSkillPoints()
        {
            return AvailableSkillPoints.CurrentValue 
                + _skills.Sum(skill => Statistics.SkillSaveValues[skill.CurrentValue]);
        }

        private void OnAttributeChanged(object sender, StatisticChangedEventArgs e)
        {
            CurrentAttributePoints += (e.NewValue - e.OldValue);
        }

        private void OnSkillChanged(object sender, StatisticChangedEventArgs e)
        {
            CurrentSkillPoints += (Statistics.SkillSaveValues[e.NewValue] - Statistics.SkillSaveValues[e.OldValue]);
        }
    }
}
