using System;
using RPG.Core;
using UnityEngine;
namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon   ", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject equippedPrefab;
        [SerializeField] AnimatorOverrideController animatorController;
        [SerializeField] float range = 2f;
        [SerializeField] float damange = 5f;
        [SerializeField] [Range(0, 10)] float timeBetweenAttacks = 1f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile Projectile;
        public bool IsRightHanded { get => isRightHanded; set => isRightHanded = value; }
        public float Range { get => range; set => range = value; }
        public float Damange { get => damange; set => damange = value; }
        public float TimeBetweenAttacks { get => timeBetweenAttacks; set => timeBetweenAttacks = value; }

        const string weaponName = "Weapon";

        private Transform GetHandTransform(Transform rightHand, Transform leftHand)
        {
            return IsRightHanded ? rightHand : leftHand;
        }

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);
            Transform handTransform = GetHandTransform(rightHand, leftHand);
            if (equippedPrefab != null && handTransform != null)
            {
                GameObject weapon = Instantiate(equippedPrefab, handTransform);
                weapon.name = weaponName;
            }
            if (animatorController != null)
            {
                animator.runtimeAnimatorController = animatorController;
            }
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if (oldWeapon == null) return;
            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        public bool HasProjectile()
        {
            return Projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Transform handTransform = GetHandTransform(rightHand, leftHand);

            Projectile projectileInstane = Instantiate(Projectile, handTransform.position, Quaternion.identity);
            projectileInstane.SetTarget(target, damange);
        }
    }
}