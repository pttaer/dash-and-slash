using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDEnemyView : MonoBehaviour
{
    [SerializeField] int m_Health = 100;

    [SerializeField] float m_Speed = 2;

    [SerializeField] Color m_HitColor = Color.red;

    [SerializeField] Transform m_TfPlayer;

    [SerializeField] LayerMask m_PlayerLayer;

    private SpriteRenderer m_Sr;

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
            Debug.Log("Did Hit: " + hit.collider.gameObject.name);
            // Handle collision with the object (e.g., attack the player)
            transform.position = Vector2.MoveTowards(transform.position, m_TfPlayer.position, m_Speed * Time.deltaTime);
        }
        else
        {
            Debug.DrawRay(raycastOrigin, raycastDirection, Color.red);
            Debug.Log("Did not Hit");
        }
    }
}
