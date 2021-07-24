using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float heathPoints = 100f;
        public bool isDead {get;set;} = false;
        Animator animator;
        ActionScheduler actionScheduler;
        private void Start()
        {
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
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
            actionScheduler.CancelCurrentAction();
        }
    }
}