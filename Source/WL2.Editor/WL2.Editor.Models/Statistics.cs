using System.Collections.Generic;

namespace WL2.Editor.Models
{
    public static class Statistics
    {
        public static readonly Dictionary<string, string> AttributeNames = new Dictionary<string, string>
        {
            {"coordination", "Coordination"},
            {"luck", "Luck"},
            {"awareness", "Awareness"},
            {"strength", "Strength"},
            {"speed", "Speed"},
            {"intelligence", "Intelligence"},
            {"charisma", "Charisma"}
        };

        public static readonly Dictionary<string, int> AttributeOrder = new Dictionary<string, int>
        {
            {"Coordination", 0},
            {"Luck", 1},
            {"Awareness", 2},
            {"Strength", 3},
            {"Speed", 4},
            {"Intelligence", 5},
            {"Charisma", 6}
        };

        public static readonly Dictionary<string, string> SkillNames = new Dictionary<string, string>
        {
            // Weapon Skills
            {"rifle", "Assault Rifles"},
            {"bladedWeapons", "Bladed Weapons"},
            {"bluntWeapons", "Blunt Weapons"},
            {"brawling", "Brawling"},
            {"energyWeapons", "Energy Weapons"},
            {"handgun", "Handguns"},
            {"atWeapons", "Heavy Weapons"},
            {"shotgun", "Shotguns"},
            {"sniperRifle", "Sniper Rifles"},
            {"smg", "Submachine Guns"},

            // General Skills
            {"animalWhisperer", "Animal Whisperer"},
            {"barter", "Barter"},
            {"bruteForce", "Brute Force"},
            {"intimidate", "Hard Ass"},
            {"spotLie", "Kiss Ass"},
            {"leadership", "Leadership"},
            {"outdoorsman", "Outdoorsman"},
            {"perception", "Perception"},
            {"manipulate", "Manipulate"},
            {"weaponSmith", "Weaponsmithing"},

            // Knowledge Skills
            {"alarmDisarm", "Alarm Disarming"},
            {"computerTech", "Computer Science"},
            {"demolitions", "Demolitions"},
            {"fieldMedic", "FieldMedic"},
            {"pickLock", "Lockpicking"},
            {"mechanicalRepair", "Mechanical Repair"},
            {"safecrack", "Safecracking"},
            {"doctor", "Surgeon"},
            {"toasterRepair", "Toaster Repair"},

            // Miscellaneous Skills
            {"calvinBackerSkill", "Southwestern Folklore"},
            {"combatShooting", "Combat Shooting"}
        };

        public static readonly Dictionary<string, SkillCategory> SkillCategories = new Dictionary<string, SkillCategory>
        {
            // Weapon Skills
            {"rifle", SkillCategory.Weapon},
            {"bladedWeapons", SkillCategory.Weapon},
            {"bluntWeapons", SkillCategory.Weapon},
            {"brawling", SkillCategory.Weapon},
            {"energyWeapons", SkillCategory.Weapon},
            {"handgun", SkillCategory.Weapon},
            {"atWeapons", SkillCategory.Weapon},
            {"shotgun", SkillCategory.Weapon},
            {"sniperRifle", SkillCategory.Weapon},
            {"smg", SkillCategory.Weapon},

            // General Skills
            {"animalWhisperer", SkillCategory.General},
            {"barter", SkillCategory.General},
            {"bruteForce", SkillCategory.General},
            {"intimidate", SkillCategory.General},
            {"spotLie", SkillCategory.General},
            {"leadership", SkillCategory.General},
            {"outdoorsman", SkillCategory.General},
            {"perception", SkillCategory.General},
            {"manipulate", SkillCategory.General},
            {"weaponSmith", SkillCategory.General},

            // Knowledge Skills
            {"alarmDisarm", SkillCategory.Knowledge},
            {"computerTech", SkillCategory.Knowledge},
            {"demolitions", SkillCategory.Knowledge},
            {"fieldMedic", SkillCategory.Knowledge},
            {"pickLock", SkillCategory.Knowledge},
            {"mechanicalRepair", SkillCategory.Knowledge},
            {"safecrack", SkillCategory.Knowledge},
            {"doctor", SkillCategory.Knowledge},
            {"toasterRepair", SkillCategory.Knowledge},

            // Miscellaneous Skills
            {"calvinBackerSkill", SkillCategory.Miscellaneous},
            {"combatShooting", SkillCategory.Miscellaneous}
        };

        public static readonly Dictionary<int, int> SkillDisplayValues = new Dictionary<int, int>
        {
            {0, 0},
            {2, 1},
            {4, 2},
            {6, 3},
            {10, 4},
            {14, 5},
            {18, 6},
            {24, 7},
            {30, 8},
            {36, 9},
            {44, 10}
        };

        public static readonly Dictionary<int, int> SkillSaveValues = new Dictionary<int, int>
        {
            {0, 0},
            {1, 2},
            {2, 4},
            {3, 6},
            {4, 10},
            {5, 14},
            {6, 18},
            {7, 24},
            {8, 30},
            {9, 36},
            {10, 44}
        };
    }
}
