using GameDevTV.Inventories;
using RPG.Shops;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.Shops
{
    public class FilterButtonUI : MonoBehaviour
    {
        [SerializeField] ItemCategory category = ItemCategory.None;
        Button button;
        Shop currentShop;
        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(selectFilter);
        }

        public void RefreshUI()
        {
            button.interactable = currentShop.GetFilter() != category;
        }


        public void SetShop(Shop currentShop)
        {
            this.currentShop = currentShop;
        }
        private void selectFilter()
        {
            currentShop.SelectFilter(category);
        }
    }
}