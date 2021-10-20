using RPG.Attribute;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        [SerializeField] float damange = 0f;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect;
        [SerializeField] float maxLifeTime = 10f;
        [SerializeField] float lifeAfterImpact = 2f;
        [SerializeField] GameObject[] destroyOnHit;
        [SerializeField] UnityEvent onHit;
        Health target;
        GameObject instagator;
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

        public void SetTarget(Health target, GameObject instagator, float damage)
        {
            this.target = target;
            this.damange += damage;
            this.instagator = instagator;
            Destroy(gameObject, maxLifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.GetComponent<Health>() != target) return;
            if (target.isDead) return;
            target.TakeDamage(instagator, damange);
            speed = 0;

            onHit.Invoke();
            if (hitEffect != null)
            {
                Instantiate(hitEffect, AimLocation, transform.rotation);

            }
            foreach (GameObject item in destroyOnHit)
            {
                Destroy(item);
            }
            Destroy(gameObject, lifeAfterImpact);

        }
    }
}

