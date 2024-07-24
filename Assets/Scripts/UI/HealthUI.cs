using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        
        public void UpdateHealth(int currentHealth, int maxHealth)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }
}