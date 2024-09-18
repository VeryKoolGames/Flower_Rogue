using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Events;
using UnityEngine;

public class ButtonEndTurnManager : MonoBehaviour
{
    [SerializeField] private GameObject endTurnButton;
    [SerializeField] private OnTurnEndListener onRedrawTurnEndListener;
    [SerializeField] private OnTurnEndListener onPlayerTurnEndListener;
    // Start is called before the first frame update

    private void OnEnable()
    {
        onRedrawTurnEndListener.Response.AddListener(EnableButton);
        onPlayerTurnEndListener.Response.AddListener(DisableButton);
    }
    
    private void EnableButton()
    {
        endTurnButton.SetActive(true);
    }

    private void DisableButton()
    {
        endTurnButton.SetActive(false);
    }
    
    private void OnDisable()
    {
        onRedrawTurnEndListener.Response.RemoveListener(EnableButton);
        onPlayerTurnEndListener.Response.RemoveListener(DisableButton);
    }
}
