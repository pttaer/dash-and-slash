using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class MDNpcView : MonoBehaviour
{
    private string m_Id;

    private SpriteRenderer m_SrPrefEmote;

    private void OnDestroy()
    {
        MDEmoteController.Api.OnShowEmoteEvent -= ShowEmote;
    }

    void Start()
    {
        m_SrPrefEmote = transform.Find("EmotePrefab").GetComponent<SpriteRenderer>();

        m_Id = Guid.NewGuid().ToString();

        MDEmoteController.Api.OnShowEmoteEvent += ShowEmote;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MDEmoteController.Api.GetEmote(m_Id, UnityEngine.Random.Range(1, 30));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_SrPrefEmote.gameObject.SetActive(false);
        }
    }

    private void ShowEmote(string id, Sprite sprite)
    {
        if(m_Id == id)
        {
            SpawnEmote(sprite);
        }
    }

    private void SpawnEmote(Sprite sprite)
    {
        m_SrPrefEmote.sprite = sprite;
        m_SrPrefEmote.gameObject.SetActive(true);
    }
}