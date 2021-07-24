using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using UnityEngine;
namespace RPG.Control
{
    public class AIControler : MonoBehaviour
    {
        [SerializeField] float ChaseDistance = 5f;
        GameObject player;
        Fighter fighter;
        Health health;
        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
        }
        private void Update()
        {
            if (health.isDead) return;
            if (InAttackRangeOfPlayer()  && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
              fighter.Cancel();
            }
        }
        private bool InAttackRangeOfPlayer()
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            return distance < ChaseDistance;
        }
    }
}
