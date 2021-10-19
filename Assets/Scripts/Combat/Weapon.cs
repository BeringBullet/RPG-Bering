using RPG.Attribute;
using UnityEngine;
namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "RPG-Bering/Weapons/Make New Weapon   ", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject equippedPrefab;
        [SerializeField] AnimatorOverrideController animatorController;
        [SerializeField] float range = 2f;
        [SerializeField] float damange = 5f;
        [SerializeField] float perventageBonus = 0f;
        [SerializeField] [Range(0, 10)] float timeBetweenAttacks = 1f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile Projectile;
        public bool IsRightHanded { get => isRightHanded; set => isRightHanded = value; }
        public float Range { get => range; set => range = value; }
        public float Damange { get => damange; set => damange = value; }
        public float PerventageBonus { get => perventageBonus; set => perventageBonus = value; }

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

            var overdideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorController != null)
            {
                animator.runtimeAnimatorController = animatorController;
            }
            else if (overdideController != null)
            {
                animator.runtimeAnimatorController = overdideController.runtimeAnimatorController;
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

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instagator, float calculatedDamage)
        {
            Transform handTransform = GetHandTransform(rightHand, leftHand);

            Projectile projectileInstane = Instantiate(Projectile, handTransform.position, Quaternion.identity);
            projectileInstane.SetTarget(target, instagator, calculatedDamage);
        }


    }
}