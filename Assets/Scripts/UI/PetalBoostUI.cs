using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PetalBoostUI : MonoBehaviour
    {
        [SerializeField] private ParticleSystem boostEffect;

        private void Awake()
        {
            boostEffect.Stop();
        }

        public void ApplyBoostingEffect()
        {
            boostEffect.Play();
        }
        
        public void RemoveBoostingEffect()
        {
            boostEffect.Stop();
        }
    }
}
