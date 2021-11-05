using RPG.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    [CreateAssetMenu(fileName = "Health Effect", menuName = "Abilities/Effect/Health", order = 0)]

    public class HealthEffect : EffectStrategy
    {
        [SerializeField] float healthChange;
  
        public override void StartEffect(AbilityData data, Action finished)
        { 
            foreach (var item in data.GetTargets())
            {
                if (healthChange < 0)
                    item.GetComponent<Health>()?.TakeDamage(data.GetUser(), -healthChange);
                else
                    item.GetComponent<Health>()?.Heal(healthChange);

            }
            finished();
        }
    }
}