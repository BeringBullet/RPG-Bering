using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float heathPoints = 100f;
        bool isDead = false;
        Animator animator;
        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        public void TakeDamage(float damage)
        {
            heathPoints = Mathf.Max(heathPoints - damage, 0);
            if (heathPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            animator.SetTrigger("die");
        }
    }
}