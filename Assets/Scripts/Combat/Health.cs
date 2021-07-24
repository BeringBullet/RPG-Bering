using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{

    public class Health : MonoBehaviour
    {
       [SerializeField] float heath = 100f;

        public void TakeDamage(float damage)
        {
            heath = Mathf.Max(heath - damage, 0);
            print(heath);
        }
    }
}
