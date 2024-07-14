using System.Collections.Generic;using Command;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour, IEntity
{
    public void Attack()
    {
        Debug.Log("Player Attack");
    }

    public void Defense()
    {
        Debug.Log("Player Defense");
    }

    public void Utility()
    {
        Debug.Log("Player Utility");
    }
}
