using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDAttackView : MonoBehaviour
{
    [SerializeField] float m_MeleeSpeed;

    [SerializeField] MDMeleePositionView m_MeleePosView;

    private Transform m_TfSword;

    private Transform m_TfSwordSprite;

    float m_TimeUntilNextAttack;

    private void Start()
    {
        m_TfSword = transform.Find("Sword");
        m_TfSwordSprite = m_TfSword.Find("GFX");
        m_MeleePosView = m_TfSword.GetComponent<MDMeleePositionView>();
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

        bool isLeftSwing = m_MeleePosView.m_Angle < 0 && m_MeleePosView.m_Angle > - 180;
        
        Debug.Log("Run here m_MeleePosView.m_Angle" + m_MeleePosView.m_Angle);

        m_TfSwordSprite.DOLocalRotate(isLeftSwing ? new Vector3(0, 0, 40) : new Vector3(0, 0, -40), 0);
        m_TfSword.DOLocalRotate(isLeftSwing ? new Vector3(0, 0, -10) : new Vector3(0, 0, 10), 0);

        m_TfSword.DOLocalRotate(new Vector3(m_TfSword.rotation.x, m_TfSword.rotation.y, isLeftSwing ? 175 : -175), 0.35f).SetEase(Ease.OutQuint).OnComplete(() =>
        {
            m_TfSwordSprite.gameObject.SetActive(false);
            m_TfSword.DOLocalRotate(new Vector3(0, 0, 0), 0);
        });
    }
}