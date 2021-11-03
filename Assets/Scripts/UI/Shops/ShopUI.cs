using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Shops;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.Shops
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI shopName;
        [SerializeField] Transform listRoot;
        [SerializeField] RowUI rowPrefab;
        [SerializeField] TextMeshProUGUI totalField;
        [SerializeField] Button confirmButton;
        [SerializeField] Button switchButton;

        Shopper shopper = null;
        Shop currentShop = null;
        Color originalTotalTextColor;

        // Start is called before the first frame update
        void Start()
        {
            originalTotalTextColor = totalField.color;
            shopper = GameObject.FindGameObjectWithTag("Player").GetComponent<Shopper>();
            if (shopper == null) return;

            shopper.activeShopChange += ShopChanged;
            confirmButton.onClick.AddListener(ConfirmTransaction);
            switchButton.onClick.AddListener(switchMode);
            ShopChanged();
        }

        private void ShopChanged()
        {
            if (currentShop != null)
            {
                currentShop.onChange -= RefreshUI;
            }
            currentShop = shopper.GetActiveShop();
            gameObject.SetActive(currentShop != null);

            foreach (FilterButtonUI button in GetComponentsInChildren<FilterButtonUI>())
            {
                button.SetShop(currentShop);
            }
            if (currentShop == null) return;
            shopName.text = currentShop.GetShopName();

            currentShop.onChange += RefreshUI;
            RefreshUI();
        }

        private void RefreshUI()
        {
            foreach (Transform item in listRoot)
            {
                Destroy(item.gameObject);
            }
            foreach (ShopItem item in currentShop.GetFilteredItems())
            {
                RowUI row = Instantiate<RowUI>(rowPrefab, listRoot);
                row.Setup(currentShop, item);
            }

            totalField.text = $"Total: ${currentShop.TransactionTotal():N2}";
            totalField.color = currentShop.HasSufficientFunds() ? originalTotalTextColor : Color.red;
            confirmButton.interactable = currentShop.CanTransact();
            TextMeshProUGUI switchText = switchButton.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI confirmText = confirmButton.GetComponentInChildren<TextMeshProUGUI>();

            switchText.text = currentShop.IsBuyingMode() ? "Switch To Selling" : "Switch To Buying";
            confirmText.text = currentShop.IsBuyingMode() ? "Buy" : "Sell";
            
            foreach (FilterButtonUI button in GetComponentsInChildren<FilterButtonUI>())
            {
                button.RefreshUI();
            }
        }

        public void Close()
        {
            shopper.SetActiveShop(null);
        }

        public void ConfirmTransaction()
        {
            currentShop.ConfirmTransaction();
        }

        public void switchMode()
        {
            currentShop.SelectMode(!currentShop.IsBuyingMode());

        }
    }
}