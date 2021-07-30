using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,99)]
       [SerializeField] int startingLevel;
       [SerializeField] CharacterClass charctorClass;
       [SerializeField] Pregression pregression;
    }
}