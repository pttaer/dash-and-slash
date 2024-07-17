using UnityEngine;
using UnityEngine.InputSystem;

public class MDPlayerMovement : MonoBehaviour
{
    private Vector2 m_Movement;
    private SpriteRenderer m_Sr;
    private Rigidbody2D m_Rb;
    [SerializeField] int m_Speed = 5;
    [SerializeField] Sprite m_FrontSprite;
    [SerializeField] Sprite m_BackSprite;

    private void Start()
    {
        m_Rb = GetComponent<Rigidbody2D>();
        m_Sr = GetComponent<SpriteRenderer>();
    }

    private void OnMovement(InputValue value)
    {
        m_Movement = value.Get<Vector2>();

        if (m_Movement.x != 0)
        {
            m_Sr.flipX = m_Movement.x < 0;
        }

        if (m_Movement.y != 0)
        {
            m_Sr.sprite = m_Movement.y > 0 ? m_BackSprite : m_FrontSprite;
        }
    }

    private void FixedUpdate()
    {
        // Movement type 1 (use time delta)
        m_Rb.MovePosition(m_Rb.position + m_Movement * m_Speed * Time.fixedDeltaTime);

        // Movement type 2 (use velocity)
        /*if (m_Movement.x != 0 || m_Movement.y != 0)
        {
            m_Rb.velocity = m_Movement * m_Speed;
        }*/

        // Movement type 3 (Add force)
        // m_Rb.AddForce(m_Movement * m_Speed);
    }
}
