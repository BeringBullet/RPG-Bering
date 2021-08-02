using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
       
       [SerializeField] [Range(1, 99)] int startingLevel;
       [SerializeField] CharacterClass charctorClass;
       [SerializeField] Progression progression;

       public float GetStat(Stat stat)
       {
           return progression.GetStat(stat, charctorClass, startingLevel);
       }

    }
}