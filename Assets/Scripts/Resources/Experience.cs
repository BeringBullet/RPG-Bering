using UnityEngine;
using RPG.Saving;
namespace RPG.Resources
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints;

        public object CaptureState()
        {
            return experiencePoints;
        }

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)experiencePoints;
        }
    }
}