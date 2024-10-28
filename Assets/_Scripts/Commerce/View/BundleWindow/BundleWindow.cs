using JustMobyTest.Commerce.Goods;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JustMobyTest.Commerce.View
{
    internal class BundleWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _headerField;
        [SerializeField] private TMP_Text _descriptionField;
        [SerializeField] private Image _mainImage;
        [SerializeField] private CommercialItemPanel _itemPanel;
        [SerializeField] private PurchaseButtonDrawer _purchaseButtonDrawer;

        private GameObject _gameObject;

        internal void Initialize() 
        {
            _gameObject = gameObject;

            _itemPanel.Initialize();
            _purchaseButtonDrawer.Initialize();
        }

        internal void Draw(string header, string description, Sprite mainSprite,
            IReadOnlyList<CommercialItem> itemsInBundle, int chosenCount, PriceTag priceTag)
        {
            _headerField.text = header;
            _descriptionField.text = description;
            _mainImage.sprite = mainSprite;
            _itemPanel.Draw(itemsInBundle, chosenCount);
            _purchaseButtonDrawer.Draw(priceTag);
            _gameObject.SetActive(true);
        }

        internal void TurnOff() 
        {
            _itemPanel.Clear();
            _gameObject.SetActive(false);
        }
    }
}