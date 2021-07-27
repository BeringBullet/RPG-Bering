using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Combat
{

    public class Fighter : MonoBehaviour, IAction
    {

        [SerializeField] Transform rightHandTransform;
        [SerializeField] Transform leftHandTransform;
        [SerializeField] Weapon defaultWeapon;

        Weapon currectWeapon;
        Health target;
        Mover mover;
        Animator animator;
        float timeSinceLastAttack = Mathf.Infinity;

        private void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();

            EquipWeapon(defaultWeapon);
        }


        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null || target.isDead) return;
            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position, 1f);
            }
            else
            {
                mover.Cancel();
                AttachBehaviour();
            }
        }
        public void EquipWeapon(Weapon weapon)
        {
            if (weapon == null) return;
            currectWeapon = weapon;
            Transform handTransform = weapon.IsRightHanded ? rightHandTransform : leftHandTransform;
            weapon.Spawn(handTransform, animator);
        }

        private void AttachBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > currectWeapon.TimeBetweenAttacks)
            {
                //This will trigger the Hit() event
                TriggerAttack();
                timeSinceLastAttack = 0;

            }
        }

        private void TriggerAttack()
        {
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");
        }

        //this is an animation event
        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(currectWeapon.Damange);
        }

        //this is an animation event
        void Shoot()
        {
            print("Fire Bow");
        }


        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currectWeapon.Range;
        }



        public void Cancel()
        {
            StopAttack();
            mover.Cancel();
            target = null;
        }

        private void StopAttack()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartACtion(this);
            target = combatTarget.GetComponent<Health>();
        }
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.isDead;
        }

    }
}