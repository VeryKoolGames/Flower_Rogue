using System;
using UnityEngine;

namespace PetalDecorator
{
    public class PetalController : MonoBehaviour
    {
        [SerializeField] private PetalDefinition petalDefinition;
        
        public IPetal petal { get; private set; }
        
        private void Awake()
        {
            petal = PetalFactory.CreatePetal(petalDefinition);
        }

        private void Start()
        {
            PetalManager.Instance.AddPetal(petal);
        }
        
        private void OnDestroy()
        {
            PetalManager.Instance.RemovePetal(petal);
        }
    }
}