using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.Traits
{
    public class TraitUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI unassignedPointText;
        [SerializeField] Button commitButton;

        TraitStore playerTraitStore = null;
        void Start()
        {
            playerTraitStore = GameObject.FindGameObjectWithTag("Player").GetComponent<TraitStore>();
            commitButton.onClick.AddListener(playerTraitStore.Commit);
        }

        void Update()
        {
            unassignedPointText.text = $"{playerTraitStore.GetUnassignedPoints()}";
        }
    }
}