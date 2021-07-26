using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{

    public class CinematicsTrigger : MonoBehaviour
    {
        bool hasTriggered = false;
        private void OnTriggerEnter(Collider other)
        {
            if (hasTriggered) return;

            if (other.tag == "Player")
            {
                hasTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}