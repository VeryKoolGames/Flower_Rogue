using System.Collections.Generic;using Command;
using DefaultNamespace;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour, IFightingEntity
{
    public void Attack(Entity target)
    {
        target.loseHP(5);
    }

    public void Defense(Entity target)
    {
        Debug.Log("Player Defense");
    }

    public void Utility(Entity target)
    {
        Debug.Log("Player Utility");
    }
}