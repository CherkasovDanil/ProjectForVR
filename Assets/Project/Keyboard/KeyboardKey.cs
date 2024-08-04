using System;
using BNG;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class KeyboardKey : MonoBehaviour 
    {
        public event EventHandler<string> OnClickButtonEvent;

        public PointerEvents PointerEvents => pointerEvents;
        
        [SerializeField] private string keyValue;
        [SerializeField] private PointerEvents pointerEvents;
        
        [SerializeField] private Material highlightMaterial;
        [SerializeField] private Material activeMaterial;
        
        [SerializeField] private bool active;
        
        private Material _initialMaterial;
        private MeshRenderer _render;
        
        // Currently hovering over the object?
        private bool _hovering = false;

        void Awake() {
            _render = GetComponent<MeshRenderer>();
            _initialMaterial = _render.sharedMaterial;
        }        

        // Holding down activate
        public void SetActive(PointerEventData eventData) {

            if (!active)
            {
                OnClickButtonEvent?.Invoke(this, keyValue);
            }
            active = true;

            UpdateMaterial();

            DOVirtual.DelayedCall(0.25f, () =>
            {
                SetInactive(null);
            });
        }

        // No longer ohlding down activate
        public void SetInactive(PointerEventData eventData) {
            active = false;

            UpdateMaterial();
        }

        // Hovering over our object
        public void SetHovering(PointerEventData eventData) {
            _hovering = true;

            UpdateMaterial();
        }

        // No longer hovering over our object
        public void ResetHovering(PointerEventData eventData) {
            _hovering = false;
            active = false;

            UpdateMaterial();
        }

        public void UpdateMaterial() {
            if (active) {
                _render.sharedMaterial = activeMaterial;
            }
            else if (_hovering) {
                _render.sharedMaterial = highlightMaterial;
            }
            else {
                _render.sharedMaterial = _initialMaterial;
            }
        }
    }
}