using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    [CreateAssetMenu(fileName = "Trigger Animation Effect", menuName = "Abilities/Effect/Trigger Animation", order = 0)]

    public class TriggerAnimation : EffectStrategy
    {
        [SerializeField] string animatorTrigger;

        public override void StartEffect(AbilityData data, Action finished)
        {
            Animator animator = data.GetUser().GetComponent<Animator>();
            animator.SetTrigger(animatorTrigger);
            finished();
        }
    }
}
