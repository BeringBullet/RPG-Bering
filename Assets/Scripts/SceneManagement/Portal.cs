using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.SceneManagement;
using RPG.Control;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 0.5f;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float fadeWaitTime = 0.5f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set.");
                yield break;
            }
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(fadeOutTime);
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            PlayerControler playerController = GameObject.FindWithTag("Player").GetComponent<PlayerControler>();
            playerController.enabled = false;

            savingWrapper.Save();
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerControler>();
            playerController.enabled = false;

            savingWrapper.Load();
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            savingWrapper.Save();
            yield return new WaitForSeconds(fadeWaitTime);
            fader.FadeIn(fadeInTime);
            playerController.enabled = true;

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            if (otherPortal == null) return;
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach (var portal in FindObjectsOfType<Portal>())
            {
                if (portal == this && portal.destination != destination) continue;
                return portal;
            }
            return null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
        }
    }
}
