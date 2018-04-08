using System;
using Exercise1_SkillTree.Models;

namespace Exercise1_SkillTree.Guides
{
    public static class CharacterGuide
    {
        public static void ShowCharacterProgression(Character character)
        {
            // Console.WriteLine(character.ToString()); // this is used to show full character info
            ShowCharacterLevelInfo(character);

            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine($"Leveling character to: {i + 2}");
                character.LevelUp();
                ShowCharacterLevelInfo(character);
            }
        }

        public static void ShowCharacterLevelInfo(Character character)
        {
            Console.WriteLine("\n\n");
            Console.WriteLine($"============== Level {character.Level}==============");
            Console.WriteLine(character.GetAvailableTalents());
        }
    }
}
