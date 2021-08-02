using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "RPG-Bering/Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses;

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                if (progressionClass.characterClass != characterClass) continue;

                foreach (progressionClass item in progressionClass.stats)
                {
                    if (item.stats != stat) continue;
                    if (item.levels.Length < level) continue;

                    return item.levels[level - 1];
                }
            }
            return 0;
        }


        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public progressionClass[] stats;
        }

        [System.Serializable]
        class progressionClass
        {
            public Stat stats;
            public float[] levels;

        }
    }
}