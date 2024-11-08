using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int currentGameState = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void StartGame()
    {
        Map.MapManager.Instance.GenerateNewMap();
    }
    
    public void ResetGame()
    {
        Map.MapManager.Instance.ResetMap();
    }
}