using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exercise1_SkillTree.Extensions;

namespace Exercise1_SkillTree.Models
{
    public class Skill
    {
        public string Name { get; set; }
        public bool IsLocked { get; set; }
        public bool CanBeUnlocked { get; set; }

        public int Level { get; set; }
        public Skill ParentSkill { get; set; }
        public List<Skill> ChildSkills { get; set; }

        public Skill AdditionalDependantSkill { get; set; }

        /*
         * Note: Even thought I didn't implement anything to check "Can this character learn this spell?" method in here.
         * I tried to create property "IsAvailable" in such a way that if we did have a need to check if spell can be used, we only need to check
         * this variable.
         */
        public bool IsAvailabe
        {
            get
            {
                if (AdditionalDependantSkill == null)
                    return CanBeUnlocked && IsLocked == false;
                return (CanBeUnlocked && IsLocked == false &&
                        (AdditionalDependantSkill != null && AdditionalDependantSkill.IsAvailabe));
            }
        }

        public Skill(string name, int level, bool isLocked = true, bool canBeUnlocked = false, Skill parentSkill = null, Skill additionalDependantSkill = null)
        {
            Name = name;
            IsLocked = isLocked;
            CanBeUnlocked = canBeUnlocked;
            ChildSkills = new List<Skill>();
            Level = level;
            ParentSkill = parentSkill;
            AdditionalDependantSkill = additionalDependantSkill;
        }

        public List<Skill> GetAvailableSkills()
        {
            List<Skill> retList = new List<Skill>();
            if (IsAvailabe)
            {
                retList.Add(this);
                if (ChildSkills.FirstOrDefault() != null && ChildSkills.First().IsAvailabe)
                {
                    foreach (var childSkill in ChildSkills)
                    {
                        retList.AddRange(childSkill.GetAvailableSkills());
                    }
                }
            }
            return retList;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(
                $"Name: {Name} - Locked: {IsLocked} - Can be unlocked: {CanBeUnlocked} - Level: {Level} - Has children: {ChildSkills.Count > 0} \n");
            if (ChildSkills.Count > 0)
            {
                for (int i = 0; i < ChildSkills.Count; i++)
                {
                    sb.Append(ChildSkills[i].ToString().Indent(Level * 4));
                }
            }
            return sb.ToString();
        }

        public void AdjustAvailableTalentsByLevel(int level)
        {
            // There are many ways to set changes to Skill class properties, I chose to do it when character levels up.
            // Takes a bit more resources than to check weather anything should be changes in first place, but imo it's less error prone.
            if (Level <= level)
            {
                IsLocked = false;
                CanBeUnlocked = true;

                for (int i = 0; i < ChildSkills.Count; i++)
                {
                    ChildSkills[i].AdjustAvailableTalentsByLevel(level);
                }
            }
        }
    }
}
