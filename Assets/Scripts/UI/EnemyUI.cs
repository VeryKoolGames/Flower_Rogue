using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class EnemyUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI armorText;
        
        public void UpdateArmor(int currentArmor)
        {
            armorText.text = "Armor: " + currentArmor;
        }
    }
}