using System.Collections;
using System.Collections.Generic;
using Bering.Core.UI.Tooltips;
using UnityEngine;

namespace RPG.UI.Quest
{
    public class QuestTooltipSpawner : TooltipSpawner
    {
        public override bool CanCreateTooltip()
        {
return true;
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
        }
    }
}

