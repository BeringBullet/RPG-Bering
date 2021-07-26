using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;
        private void Start()
        {
            player = GameObject.FindWithTag("Player");

            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }


        void DisableControl(PlayableDirector pd)
        {
            if (player == null) return;
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerControler>().enabled = false;
        }

        void EnableControl(PlayableDirector pd)
        {
            if (player == null) return;
            player.GetComponent<PlayerControler>().enabled = true;
        }
    }
}