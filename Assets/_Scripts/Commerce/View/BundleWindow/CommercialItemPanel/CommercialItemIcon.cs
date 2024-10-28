using JustMobyTest.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JustMobyTest.Commerce.View
{
    internal class CommercialItemIcon : MonoBehaviour, IMonoBehaviourPoolElement
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _quantityField;

        private Transform _transform;
        public GameObject GameObject { get; private set; }

        public void Awake()
        {
            GameObject = gameObject;
            _transform = transform;
        }

        internal void Draw(Sprite sprite, int quantity) 
        {
            _image.sprite = sprite;
            _quantityField.text = quantity.ToReadableInt();
            GameObject.SetActive(true);
        }

        internal void SetParent(Transform parent) 
        {
            _transform.SetParent(parent);
        }
    }
}