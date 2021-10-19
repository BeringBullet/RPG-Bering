using RPG.Core;
using RPG.Attribute;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;
using System.Collections.Generic;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float MaxSpeed = 6f;
        NavMeshAgent navMeshAgent;
        Animator animator;
        Health health;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            navMeshAgent.enabled = !health.isDead;
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", speed);
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }
        public void StartMoveAction(Vector3 destination, float speedFration)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFration);
        }

        public void MoveTo(Vector3 destination, float speedFration)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = MaxSpeed * Mathf.Clamp01(speedFration);
            navMeshAgent.isStopped = false;
        }

        [System.Serializable]
        struct MoverSaveData
        {
            public SerializableVector3 position;
            public SerializableVector3 rotation;
        }

        public object CaptureState()
        {
            MoverSaveData data = new MoverSaveData();
            data.position = transform.position;
            data.rotation = transform.eulerAngles;

            return data;
        }

        //this gets call before start so we need to look up NavemeshAgent
        public void RestoreState(object state)
        {
            MoverSaveData data = (MoverSaveData)state;
            GetComponent<NavMeshAgent>().Warp(data.position);
            transform.eulerAngles = data.rotation;
        }
    }
}