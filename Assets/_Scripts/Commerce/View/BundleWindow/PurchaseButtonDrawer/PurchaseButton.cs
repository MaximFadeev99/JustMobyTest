using System.Globalization;
using TMPro;
using UnityEngine;

namespace JustMobyTest.Commerce.View
{
    internal class PurchaseButton : MonoBehaviour
    {
        [field: SerializeField] protected TMP_Text _priceField;

        private GameObject _gameObject;

        internal void Initialize() 
        {
            _gameObject = gameObject;
        }

        internal void DrawPrice(float price) 
        {
            _priceField.text = $"{price.ToString("C", CultureInfo.CurrentCulture)}";
            _gameObject.SetActive(true);
        }

        internal void TurnOff() 
        {
            _gameObject.SetActive(false);
        }
    }
}