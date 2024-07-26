using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDEnemyView : MonoBehaviour
{
    [SerializeField] int m_Health = 100;

    [SerializeField] float m_Speed = 2;

    [SerializeField] Color m_HitColor = Color.red;

    [SerializeField] Transform m_TfPlayer; // Also rotation center

    [SerializeField] LayerMask m_PlayerLayer;

    private SpriteRenderer m_Sr;

    [SerializeField] bool m_IsNpc = true;

    [SerializeField] CircleCollider2D m_SpecificCollider;

    [SerializeField] float m_DashTimeout = 0;

    private bool m_IsDashing = false;

    [SerializeField] float m_StopDistance = 4f;

    [SerializeField] float m_RotationRadius = 4f;

    [SerializeField] float m_AngularSpeed = 2f;

    private float m_PosX, m_PosY, m_Angle = 0f;

    private void Start()
    {
        m_Sr = GetComponent<SpriteRenderer>();
        m_PlayerLayer = LayerMask.GetMask("Player");
    }

    public void TakeDamage(int damageTaken)
    {
        if (m_Health > damageTaken)
        {
            m_Health -= damageTaken;

            m_Sr.DOColor(m_HitColor, 0.1f).SetEase(Ease.InOutExpo).OnComplete(() =>
            {
                m_Sr.DOColor(Color.white, 0.1f).SetEase(Ease.InOutExpo);
            });

            Debug.Log("A big hit " + damageTaken);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_SpecificCollider != null && collision == m_SpecificCollider)
        {
            
        }
    }

    private void Dash()
    {
        m_Sr.DOColor(Color.blue, 0.1f).SetEase(Ease.InOutExpo).OnComplete(() =>
        {
            transform.position = Vector2.Lerp(transform.position, m_TfPlayer.position, m_Speed);
            Debug.Log("Run here DASH");
            m_DashTimeout = 5f;
            m_IsDashing = true;
            m_Sr.DOColor(Color.white, 0.1f).SetEase(Ease.InOutExpo);
        });
    }

    void Update() // Or a suitable update function based on your game logic
    {
        // Raycast from enemy position towards player in 2D space
        Vector2 raycastOrigin = transform.position;
        Vector2 raycastDirection = new Vector2(m_TfPlayer.position.x, m_TfPlayer.position.y) - raycastOrigin;

        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, raycastDirection);

        // Handle the hit and miss cases    

        if (hit.collider.CompareTag("Player"))
        {
            Debug.DrawRay(raycastOrigin, raycastDirection * hit.distance, Color.blue);

            //Debug.Log("Did Hit: " + hit.collider.gameObject.name);
            //Debug.Log("Run here " + Vector2.Distance(transform.position, m_TfPlayer.position));

            if (Vector2.Distance(transform.position, m_TfPlayer.position) < m_StopDistance)
            {
                RotateAroundPlayer();
            }
            else
            {
                if (!m_IsDashing && m_DashTimeout < 0)
                {
                    Dash();
                }
                else
                {
                    MoveToPlayer();
                }
            }
        }
        else
        {
            Debug.DrawRay(raycastOrigin, raycastDirection, Color.red);
            Debug.Log("Did not Hit");
            if(m_IsDashing)
            {
                m_IsDashing = false;
            }
        }
    }

    private void RotateAroundPlayer()
    {
        //m_AngularSpeed = Random.Range(-0.5f, 0.5f);

        m_PosX = m_TfPlayer.position.x + Mathf.Cos(m_Angle) * m_RotationRadius;
        m_PosY = m_TfPlayer.position.y + Mathf.Sin(m_Angle) * m_RotationRadius;

        transform.position = Vector2.Lerp(transform.position, new Vector2(m_PosX, m_PosY), m_Speed * Time.deltaTime);

        m_Angle = m_Angle + Time.deltaTime * m_AngularSpeed;

        if (m_Angle >= 360f)
        {
            m_Angle = 0;
        }
    }

    private void MoveToPlayer()
    {
        // Handle collision with the object (e.g., attack the player)
        transform.position = Vector2.Lerp(transform.position, m_TfPlayer.position, m_Speed * Time.deltaTime);

        m_DashTimeout -= Time.deltaTime;

        if (m_DashTimeout < 0)
        {
            m_IsDashing = false;
        }
    }
}
