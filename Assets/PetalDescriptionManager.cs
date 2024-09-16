using PetalAttacks;
using TMPro;
using UnityEngine;

public class PetalDescriptionManager : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    private string description;
    // Start is called before the first frame update

    private void Start()
    {
        description = GetComponent<PlayerMove>().PetalSo.petalAttributes.description;
    }

    public void SetDescription()
    {
        descriptionText.text = description;
    }
    
    public void ClearDescription()
    {
        descriptionText.text = "";
    }
}
