using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> actionPoints = new List<GameObject>();
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private OnTurnEndListener onTurnEndListener;
    
    private void Start()
    {
        onTurnEndListener.Response.AddListener(ActivateAllActionPoints);
    }
    public void UpdateActionPoints(int currentPoints)
    {
        for (int i = 0; i < actionPoints.Count; i++)
        {
            actionPoints[i].GetComponent<RawImage>().color = i < currentPoints ? Color.green : Color.red;
        }
    }
    
    private void OnDisable()
    {
        onTurnEndListener.Response.RemoveListener(ActivateAllActionPoints);
    }
    
    private void ActivateAllActionPoints()
    {
        foreach (var actionPoint in actionPoints)
        {
            actionPoint.GetComponent<RawImage>().color = Color.green;
        }
    }
    
    public void UpdateArmor(int currentArmor)
    {
        armorText.text = "Armor: " + currentArmor;
    }
}
