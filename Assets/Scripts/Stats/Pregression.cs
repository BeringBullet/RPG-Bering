using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Pregression", menuName = "RPG-Bering/Pregression", order = 0)]
    public class Pregression : ScriptableObject
    {
        [SerializeField] PregressionClass[] characterClass;

        [System.Serializable]
        class PregressionClass
        {
            [SerializeField] characterClass characterClass;
            [SerializeField] float[] health;
        }
    }
}