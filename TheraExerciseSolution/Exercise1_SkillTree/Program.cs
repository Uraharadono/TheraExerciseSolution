using System;
using Exercise1_SkillTree.Guides;
using Exercise1_SkillTree.Models;

namespace Exercise1_SkillTree
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Mage character variables*/
            Character Mage = new Character("Mage");
            Skill Fireball = new Skill("Fireball", 1, false, true);
            Skill Electroshock = new Skill("Electroshock", 2, canBeUnlocked: false, parentSkill: Fireball);
            Skill Freeze = new Skill("Freeze", 2, canBeUnlocked: false, parentSkill: Fireball);
            Skill Thunderbolt = new Skill("Thunderbolt", 3, false, parentSkill: Electroshock);
            Skill Snowstorm = new Skill("Snowstorm", 3, false, parentSkill: Freeze);

            Electroshock.ChildSkills.Add(Thunderbolt);
            Freeze.ChildSkills.Add(Snowstorm);
            Fireball.ChildSkills.Add(Electroshock);
            Fireball.ChildSkills.Add(Freeze);

            Mage.Skills.Add(Fireball);


            /* Warrior character variables */
            Character Warrior = new Character("Warrior");
            Skill Strike = new Skill("Strike", 1, false, true);
            Skill Hit = new Skill("Hit", 1, false, true);
            Skill DoubleStrike = new Skill("Double Strike", 2, canBeUnlocked: false, parentSkill: Strike);
            Skill Slash = new Skill("Slash", 2, false, parentSkill: Strike);
            Skill Knockout = new Skill("Knockout", 2, false, parentSkill: Hit);
            Skill RoundhouseKick = new Skill("Roundhouse Kick", 3, true, parentSkill: Slash, additionalDependantSkill: Knockout);

            Slash.ChildSkills.Add(RoundhouseKick);
            Knockout.ChildSkills.Add(RoundhouseKick);
            Strike.ChildSkills.Add(DoubleStrike);
            Strike.ChildSkills.Add(Slash);
            Hit.ChildSkills.Add(Knockout);

            Warrior.Skills.Add(Strike);
            Warrior.Skills.Add(Hit);

            Console.WriteLine("=================================");
            Console.WriteLine("============== BEGIN ============");
            Console.WriteLine("=================================\n");

            CharacterGuide.ShowCharacterProgression(Mage);
            CharacterGuide.ShowCharacterProgression(Warrior);

            Console.WriteLine("=================================");
            Console.WriteLine("============== END ==============");
            Console.WriteLine("=================================\n");
        }
    }
}
