using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Destroyer : MonoBehaviour
    {
       [SerializeField] GameObject targetToDestroy;

        public void DestroyTarget() {
            Destroy(targetToDestroy);
        }
    }
}

