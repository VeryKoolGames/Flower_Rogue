using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> actionPoints = new List<GameObject>();

    public void UpdateActionPoints(int currentPoints)
    {
        for (int i = 0; i < actionPoints.Count; i++)
        {
            actionPoints[i].GetComponent<RawImage>().color = i < currentPoints ? Color.green : Color.red;
        }
    }
}
