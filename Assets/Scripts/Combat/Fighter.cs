using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Resources;
using RPG.Saving;
using RPG.Stats;
using System.Collections.Generic;

namespace RPG.Combat
{

    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
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
            if (currectWeapon == null)
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
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public Health GetTarget()
        {
            return target;
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

            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            if (currectWeapon.HasProjectile())
            {
                currectWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, damage);
            }
            else
            {
                target.TakeDamage(gameObject, damage);
            }
        }

        //this is an animation event
        void Shoot()
        {
            Hit();
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


        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currectWeapon.Damange;
            }
        }


        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.isDead;
        }

        public object CaptureState()
        {
            return currectWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = UnityEngine.Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }

    }
}