using BNG;
using UnityEngine;

namespace DefaultNamespace
{
    public class CurstomGrabbablePrefab : Grabbable
    {
        [SerializeField] private Material grabMaterial;
        [SerializeField] private MeshRenderer meshRenderer;
        
        private Material _baseMaterial;

        private void Start()
        {
            _baseMaterial = meshRenderer.material;
        }

        public override void GrabItem(Grabber grabbedBy)
        {
            base.GrabItem(grabbedBy);
            
            meshRenderer.material = grabMaterial;
        }

        public override void DropItem(Grabber droppedBy)
        {
            base.DropItem(droppedBy);

            meshRenderer.material = _baseMaterial;
        }
    }
}