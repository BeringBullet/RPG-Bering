using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{

    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamange = 5f;
        [SerializeField] [Range(0, 10)] float timeBetweenAttacks = 1f;

        Transform target;
        Mover mover;
        Animator animator;
        float timeSinceLastAttack = 0;

        private void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (!GetIsInRange())
            {
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Cancel();
                AttachBehaviour();
            }
        }

        private void AttachBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                //This will trigger the Hit() event
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0;
                
            }
        }

        //this is an animation event
        void Hit()
        {
            Health healthComponent = target.GetComponent<Health>();
            if (healthComponent != null)
                healthComponent.TakeDamage(weaponDamange);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartACtion(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }

        
    }
}