using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDAttackView : MonoBehaviour
{
    [SerializeField] float m_MeleeSpeed;

    [SerializeField] int m_NormalAttackDamage = 50;

    private Transform m_TfSword;

    private Transform m_TfSwordSprite;

    float m_TimeUntilNextAttack;

    private void Start()
    {
        m_TfSword = transform.Find("Sword");
        m_TfSwordSprite = m_TfSword.Find("GFX");
    }

    void Update()
    {
        if(m_TimeUntilNextAttack <= 0f)
        {
            if(Input.GetMouseButtonDown(0))
            {
                NormalAttack();
                m_TimeUntilNextAttack = m_MeleeSpeed;
            }
        }
        else
        {
            m_TimeUntilNextAttack -= Time.deltaTime;
        }
    }

    private void NormalAttack()
    {
        m_TfSwordSprite.gameObject.SetActive(true);

        float currentRotation = m_TfSword.rotation.z;

        Debug.Log("Run here " + currentRotation);

        bool isLeftSwing = currentRotation > 0;

        m_TfSwordSprite.DORotate(isLeftSwing ? new Vector3(0, 0, -40) : new Vector3(0, 0, 40), 0); 

        m_TfSword.DORotate(new Vector3(m_TfSword.rotation.x, m_TfSword.rotation.y, isLeftSwing ? currentRotation + 110 : currentRotation - 110), 0.15f).SetEase(Ease.InOutExpo).OnComplete(() =>
        {
            m_TfSwordSprite.gameObject.SetActive(false);
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<MDEnemyView>().TakeDamage(m_NormalAttackDamage);
            Debug.Log("Run here " + collision.tag);
        }
    }
}
