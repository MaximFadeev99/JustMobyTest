using JustMobyTest.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace JustMobyTest.Commerce.View
{
    [RequireComponent(typeof(Toggle))]
    public class AssortmentIcon : MonoBehaviour, IMonoBehaviourPoolElement
    {
        [SerializeField] private Image _image;

        private Toggle _toggle;

        public string BundleName { get; private set; }
        public GameObject GameObject { get; private set; }

        public void Awake()
        {
            GameObject = gameObject;
            _toggle = GetComponent<Toggle>();
        }

        internal void SetUp(ToggleGroup toggleGroup, string bundleName, Sprite sprite) 
        {
            _image.sprite = sprite;
            _toggle.group = toggleGroup;
            BundleName = bundleName;
            GameObject.SetActive(true);
        }

        internal void TurnOff() 
        {
            GameObject.SetActive(false);
        }
    }
}