using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon;
        [SerializeField] float respownTime = 5f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                GameObject player = GameObject.FindWithTag("Player");
                player.GetComponent<Fighter>().EquipWeapon(weapon);
                StartCoroutine(HideForSeconds(respownTime));
            }
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        private void ShowPickup(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }
    }
}