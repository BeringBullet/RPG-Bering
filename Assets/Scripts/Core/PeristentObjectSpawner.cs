
using System;
using UnityEngine;

namespace RPG.Core
{
    public class PeristentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject peristentObjectPrefab;

        static bool hasSpawned = false;
        private void Awake()
        {
            if (hasSpawned) return;
            SpawnPersistentObjects();
            hasSpawned = true;
        }

        private void SpawnPersistentObjects()
        {
            GameObject peristentObject = Instantiate(peristentObjectPrefab);
            peristentObject.name = "PeristentObject";
            DontDestroyOnLoad(peristentObject);
        }
    }
}