using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDEnemyView : MonoBehaviour
{
    [SerializeField] int m_Health = 100;

    [SerializeField] float m_Speed = 2;

    [SerializeField] Color m_HitColor = Color.red;

    private SpriteRenderer m_Sr;

    private void Start()
    {
        m_Sr = GetComponent<SpriteRenderer>();
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
}
