using Command;
using PetalAttacks;
using ScriptableObjectScripts;
using TMPro;
using UnityEngine;

namespace PetalBehaviors
{
    public class PetalDescriptionManager : MonoBehaviour
    {
        public TextMeshProUGUI descriptionText;
        private string description;
        private string warningDescription;
        // Start is called before the first frame update

        private void Start()
        {
            description = GetComponent<PlayerMove>().PetalSo.petalAttributes.description;
            warningDescription = GetComponent<PlayerMove>().PetalSo.petalAttributes.warningDescription;
        }

        public void SetDescription()
        {
            descriptionText.text = description;
            SetWarningDescription();
            SetDamageDescription();
        }
    
        public void ClearDescription()
        {
            descriptionText.text = "";
        }
        
        private void SetWarningDescription()
        {
            if (warningDescription != null)
            {
                descriptionText.text += "\n<color=red>" + warningDescription + "</color>";
            }
        }
        
        private void SetDamageDescription()
        {
            if (GetComponent<PlayerMove>() is IKeepPlayerReference playerReference)
            {
                descriptionText.text += "\n<color=lightblue> Damage: " + playerReference.player.GetArmor() + "</color>";
            }
            else if (GetComponent<PlayerMove>() is PlayerAttackMove playerAttack)
            {
                int active = playerAttack.activeValue + playerAttack.boostCount;
                descriptionText.text += "\n<color=lightblue> Active value: " + active + "</color>";
                if (playerAttack.passiveValue != 0)
                {
                    int passive = playerAttack.passiveValue + playerAttack.boostCount;
                    descriptionText.text += "\n<color=lightblue> Passive value: " + passive + "</color>";
                }
            }
            else if (GetComponent<PlayerMove>() is PlayerBoostMove playerBoost)
            {
                descriptionText.text += "\n<color=lightblue> Boosts for: " + playerBoost.petalBoostSo.boostAmount + "</color>";
            }
        }
    }
}
