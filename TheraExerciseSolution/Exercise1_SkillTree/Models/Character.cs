using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exercise1_SkillTree.Extensions;

namespace Exercise1_SkillTree.Models
{
    public class Character
    {
        public string Name { get; set; }
        public List<Skill> Skills { get; set; }
        public int Level { get; set; }

        public Character(string name)
        {
            Name = name;
            Level = 1;
            Skills = new List<Skill>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name);
            sb.Append("\n");
            foreach (var skill in Skills)
            {
                sb.Append(skill.ToString());
            }

            return sb.ToString();
        }

        public string GetAvailableTalents()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Available talents: \n");
            List<Skill> availableSkills = Skills.Where(s => !s.IsLocked).ToList();

            for (int i = 0; i < availableSkills.Count; i++)
            {
                // sb.Append(availableSkills[i].Name.Indent(availableSkills[i].Level * 3) + "\n");
                List<Skill> availableChildSkills = availableSkills[i].GetAvailableSkills();
                for (int j = 0; j < availableChildSkills.Count; j++)
                {
                    sb.Append(availableChildSkills[j].Name.Indent(availableChildSkills[j].Level * 3) + "\n");
                }
            }

            return sb.ToString();
        }

        public void LevelUp()
        {
            ++Level;
            for (int i = 0; i < Skills.Count; i++)
            {
                Skills[i].AdjustAvailableTalentsByLevel(Level);
            }

        }
    }
}
