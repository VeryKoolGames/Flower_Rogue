using Command;
using DefaultNamespace;
using DefaultNamespace.Events;
using PetalAttacks;
using UnityEngine;

public class SelectPetalTarget : MonoBehaviour
{
    [SerializeField] private OnTargetUpdateEvent onTargetUpdateEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            onTargetUpdateEvent.Raise(GetComponent<IFightingEntity>().commandPick, new Entity[] { other.GetComponent<Enemy.Enemy>() });
        }
    }
}
