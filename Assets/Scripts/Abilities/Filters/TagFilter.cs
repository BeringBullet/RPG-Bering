using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities.Filters
{
    [CreateAssetMenu(fileName = "Tag Filter", menuName = "Abilities/Filter/Tag", order = 0)]

    public class TagFilter : FilterStrategy
    {
        [SerializeField] string tagToFilter = "";
        public override IEnumerable<GameObject> Filter(IEnumerable<GameObject> objectsToFilter)
        {
            foreach (GameObject item in objectsToFilter)
            {
                if (item.tag == tagToFilter)
                {
                    yield return item;
                }
            }
        }
    }
}