using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.InputSystem;

public class MDAttackView : MonoBehaviour
{
    [SerializeField] float m_MeleeSpeed;

    private MDMeleePositionView m_MeleePosView;

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

        // Assuming m_MeleePosView.m_Angle is in degrees

        // Change the rotation of the sword base on the direction of mouse to player
        Vector3 swingRotation = new Vector3(m_TfSword.rotation.eulerAngles.x, m_TfSword.rotation.eulerAngles.y, m_MeleePosView.m_Angle - 90);

        m_TfSword.DOLocalRotate(swingRotation, 0.35f).SetEase(Ease.OutQuint).OnComplete(() =>
        {
            m_TfSwordSprite.gameObject.SetActive(false);
            m_TfSword.DOLocalRotate(new Vector3(0, 0, 0), 0);
        });
    }
}