using GameDevTV.Inventories;
using System;
using TMPro;
using UnityEngine;

namespace RPG.Shops
{
    public class ShopItem
    {
        InventoryItem item;
        int availability;
        float price;
        int quantityInTransaction;

        public ShopItem(
            InventoryItem item, 
            int availability,
            float price,
            int quantityInTransaction)
        {
            this.item = item;
            this.availability = availability;
            this.price = price;
            this.quantityInTransaction = quantityInTransaction;
        }

        public int getAvailability()
        {
            return availability;
        }

        public Sprite getIcon()
        {
            return item.GetIcon();
        }

        public string GetName()
        {
            return item.GetDisplayName();
        }

        public float GetPrice()
        {
            return price;
        }

        public int GetQuantityInTransaction()
        {
            return quantityInTransaction;
        }

        public InventoryItem GetInventoryItem()
        {
            return item;
        }
    }
}