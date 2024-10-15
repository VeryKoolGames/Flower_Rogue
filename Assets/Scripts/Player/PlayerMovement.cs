using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 5f;
        private Vector2 movement;
        private Rigidbody2D rb;
        private bool canMove = true;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        
        public void SetCanMove(bool canMove)
        {
            this.canMove = canMove;
        }

        void Update()
        {
            if (!canMove)
            {
                movement = Vector2.zero;
                return;
            }
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement = movement.normalized;
        }

        void FixedUpdate()
        {
            Vector2 newPosition = rb.position + movement * (moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
        }
    }
}