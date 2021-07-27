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

        public void Spawn(Transform handTransform, Animator animator)
        {
            if (equippedPrefab != null && handTransform != null)
            {
                Instantiate(equippedPrefab, handTransform);
            }
            if (animatorController != null)
            {
                animator.runtimeAnimatorController = animatorController;
            }
        }

        public bool HasProjectile()
        {
            return Projectile != null;
        }

        public void LaunchProjectile(Transform handTransform, Health target)
        {
            Projectile projectileInstane = Instantiate(Projectile, handTransform.position, Quaternion.identity);
            projectileInstane.SetTarget(target, damange);
        }
    }
}