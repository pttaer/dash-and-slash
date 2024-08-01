using DG.Tweening;
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
    [SerializeField] ParticleSystem m_RunParticle;

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
            bool isMovingLeft = m_Movement.x < 0;
            m_Sr.flipX = isMovingLeft;
            m_RunParticle.gameObject.transform.DOLocalMoveX(isMovingLeft ? 0.4f : -0.4f, 0f);
            m_RunParticle.gameObject.transform.DOScaleX(isMovingLeft ? 1 : -1, 0f);
            m_RunParticle.Play();
        }
        else
        {
            if (m_RunParticle.isPlaying)
            {
                m_RunParticle.Stop();
            }
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
