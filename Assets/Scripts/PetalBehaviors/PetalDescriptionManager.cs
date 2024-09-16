using PetalAttacks;
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
    }
}
