using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quest
{
    
    [CreateAssetMenu(fileName = "Quest", menuName = "Quests/Quest", order = 0)]
    public class Quest : ScriptableObject {
        [SerializeField] string questName;
        [SerializeField] string[] objectives;
    }

}