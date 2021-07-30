using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
       
       [SerializeField] [Range(1, 99)] int startingLevel;
       [SerializeField] CharacterClass charctorClass;
       [SerializeField] Pregression pregression;

       public float GetHealth()
       {
           return 0;
       }
    }
}