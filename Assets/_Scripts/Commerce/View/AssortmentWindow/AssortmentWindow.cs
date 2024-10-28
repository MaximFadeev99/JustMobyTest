using JustMobyTest.Commerce.Goods;
using JustMobyTest.Utilities;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JustMobyTest.Commerce.View 
{
    internal class AssortmentWindow : MonoBehaviour
    {
        [SerializeField] private AssortmentIcon _assortmentIconPrefab;
        [SerializeField] private Transform _content;
        [SerializeField] private ToggleGroup _toggleGroup;
        [SerializeField] private TMP_Text _countField;

        private GameObject _gameObject;
        private MonoBehaviourPool<AssortmentIcon> _iconPool;

        private readonly List<AssortmentIcon> _activeIcons = new List<AssortmentIcon>();

        internal void Initialize()
        {
            _gameObject = gameObject;
            _iconPool = new MonoBehaviourPool<AssortmentIcon>(_assortmentIconPrefab, _content, 10);
        }

        internal void Draw(IReadOnlyList<CommercialBundle> availableBundles)
        {
            foreach (CommercialBundle bundle in availableBundles)
            {
                AssortmentIcon idleIcon = _iconPool.GetIdleElement();
                idleIcon.SetUp(_toggleGroup, bundle.Name, bundle.MainSprite);
                _activeIcons.Add(idleIcon);
            }
        }

        internal void DrawCountField(int currentCount)
        {
            _countField.text = currentCount.ToString();
        }

        internal void Clear()
        {
            Toggle activeToggle = _toggleGroup.GetFirstActiveToggle();

            if (activeToggle != null)
                activeToggle.isOn = false;

            foreach (AssortmentIcon icon in _activeIcons)
                icon.TurnOff();

            _activeIcons.Clear();
        }

        internal void SetActive(bool isActive)
        {
            _gameObject.SetActive(isActive);
        }
    }
}