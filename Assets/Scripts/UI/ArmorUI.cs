using TMPro;
using UnityEngine;

namespace UI
{
    public class ArmorUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI armorText;
    
        public void UpdateArmor(int currentArmor)
        {
            armorText.text = "Armor: " + currentArmor;
        }
    }
}
