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
        [SerializeField] bool isHoming = false;
        Health target;

        Vector3 AimLocation
        {
            get
            {
                CapsuleCollider targetCollider = target.GetComponent<CapsuleCollider>();
                if (targetCollider == null) return 
                target.transform.position;
                return target.transform.position + Vector3.up * targetCollider.height / 2;
            }
        }

        private void Start()
        {
            transform.LookAt(AimLocation);
        }

        // Update is called once per frame
        void Update()
        {
            if (target == null) return;
            if (isHoming && !target.isDead) transform.LookAt(AimLocation);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damange += damage;
        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.GetComponent<Health>() != target) return;
            if (target.isDead) return;
            target.TakeDamage(damange);
            Destroy(gameObject);
        }
    }
}

