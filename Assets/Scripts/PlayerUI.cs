using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> actionPoints = new List<GameObject>();
    [SerializeField] private TextMeshProUGUI armorText;

    public void UpdateActionPoints(int currentPoints)
    {
        for (int i = 0; i < actionPoints.Count; i++)
        {
            actionPoints[i].GetComponent<RawImage>().color = i < currentPoints ? Color.green : Color.red;
        }
    }
    
    public void UpdateArmor(int currentArmor)
    {
        armorText.text = "Armor: " + currentArmor;
    }
}
