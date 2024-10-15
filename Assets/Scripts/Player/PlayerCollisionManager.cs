using Collision;
using UnityEngine;

namespace Player
{
    public class PlayerCollisionManager : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        
        private void Awake()
        {
            _playerMovement = GetComponentInParent<PlayerMovement>();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Pnj") || other.CompareTag("ExitToCombat"))
            {
                other.GetComponent<IOnPlayerCollision>().OnPlayerCollisionEnter(_playerMovement);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Pnj") || other.CompareTag("ExitToCombat"))
            {
                other.GetComponent<IOnPlayerCollision>().OnPlayerCollisionExit();
            }
        }
    }
}
