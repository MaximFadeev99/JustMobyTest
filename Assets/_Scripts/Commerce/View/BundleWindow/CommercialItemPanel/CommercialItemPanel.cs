using JustMobyTest.Commerce.Goods;
using JustMobyTest.Settings;
using JustMobyTest.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JustMobyTest.Commerce.View
{
    internal class CommercialItemPanel : MonoBehaviour
    {
        private const int MaxIconsPerSocket = 3;

        [SerializeField] private CommercialItemIcon _iconPrefab;
        [SerializeField] private List<Transform> _sockets;

        private readonly List<CommercialItemIcon> _activeIcons = new List<CommercialItemIcon>();
        
        private MonoBehaviourPool<CommercialItemIcon> _iconPool;
        private Transform _transform;

        internal void Initialize() 
        {
            _transform = transform;
            _iconPool = new MonoBehaviourPool<CommercialItemIcon>(_iconPrefab, _transform, 
                GameSettings.MaxItemsInCommercialBundle);
        }

        internal void Draw(IReadOnlyList<CommercialItem> itemsToDraw, int chosenCount) 
        {
            if (itemsToDraw.Count > GameSettings.MaxItemsInCommercialBundle) 
            {
                CustomLogger.Log(nameof(CommercialItemPanel), $"You have requested to draw " +
                    $"{itemsToDraw.Count} {nameof(CommercialItem)}s, but the current limit is " +
                    $"{GameSettings.MaxItemsInCommercialBundle}. Change the limit or the requested amount!", MessageTypes.Warning);

                return;
            }

            foreach (CommercialItem item in itemsToDraw) 
            {
                CommercialItemIcon idleIcon = _iconPool.GetIdleElement();

                idleIcon.Draw(item.Sprite, item.Quantity * chosenCount);
                SetAppropriateSocket(idleIcon);
                _activeIcons.Add(idleIcon);
            }
        }

        internal void SetAppropriateSocket(CommercialItemIcon unassignedIcon) 
        {
            Transform firstNotFilledSocket = _sockets
                .First(socket => socket.childCount < MaxIconsPerSocket);

            unassignedIcon.SetParent(firstNotFilledSocket);
        }

        internal void Clear() 
        {
            foreach (CommercialItemIcon icon in _activeIcons) 
            {
                icon.SetParent(_transform);
                icon.GameObject.SetActive(false);
            }

            _activeIcons.Clear();
        }
    }
}