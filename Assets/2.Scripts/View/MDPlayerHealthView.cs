using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDPlayerHealthView : MonoBehaviour
{
    [SerializeField] int m_CurrentHP;
    [SerializeField] int m_MaxHP;
    private SpriteRenderer m_Sr;

    private void OnDestroy()
    {
        MDDamageController.Api.OnDamageToPlayerEvent -= GetDamage;
    }

    private void Start()
    {
        MDDamageController.Api.OnDamageToPlayerEvent += GetDamage;
        m_Sr = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// This method take damage from gameobject having <see cref="MDDamagePlayerView"/> script.
    /// </summary>
    private void GetDamage(int damageRecived)
    {
        if (damageRecived > m_CurrentHP)
        {
            m_CurrentHP = 0;
            Time.timeScale = 0;
            MDDamageController.Api.ShowDamagedColor(m_Sr, () =>
            {
                Destroy(gameObject);
            });
            Debug.Log("YOU DIED!");
        }
        else
        {
            m_CurrentHP -= damageRecived;
            Debug.Log("OUCH!");
            MDDamageController.Api.ShowDamagedColor(m_Sr);
        }
    }
}
