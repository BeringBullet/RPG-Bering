using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        [SerializeField] float damange = 0f;
        Health target;

        Vector3 AimLocation
        {
            get
            {
                CapsuleCollider targetCollider = target.GetComponent<CapsuleCollider>();
                if (targetCollider == null) return target.transform.position;
                return target.transform.position + Vector3.up * targetCollider.height / 2;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (target == null) return;
            transform.LookAt(AimLocation);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damange += damage;
        }

        private void OnTriggerEnter(Collider other) {

            Health healthTarget = other.GetComponent<Health>();
            if (healthTarget != null && healthTarget == target)
            {
                other.GetComponent<Health>().TakeDamage(damange);
                Destroy(gameObject);
            }
        }
    }
}

