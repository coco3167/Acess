using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Platformer
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlatformerController : MonoBehaviour
    {
        [SerializeField] private float jumpForce;
    
        private Rigidbody2D m_rigidbody;
        private bool m_isGrounded, m_isJumping;
        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
                m_isGrounded = true;
        }

        private void FixedUpdate()
        {
            if(m_isJumping && m_isGrounded)
                Jump();
        }
    
        private void OnInteract(InputValue value)
        {
            m_isJumping = value.isPressed;
        }

        private void Jump()
        {
            m_isGrounded = false;
            m_rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        public void HitObstacle()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
