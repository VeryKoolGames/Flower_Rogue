using System.Collections.Generic;
using PetalDecorator;
using UnityEngine;

public class PetalManager : MonoBehaviour
{
    public static PetalManager Instance;
    private List<IPetal> petals = new List<IPetal>();
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    public void AddPetal(IPetal petal)
    {
        petals.Add(petal);
    }
    
    public void RemovePetal(IPetal petal)
    {
        petals.Remove(petal);
    }
}
