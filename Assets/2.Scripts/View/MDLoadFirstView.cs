using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDLoadFirstView : MonoBehaviour
{
    // This script is for loading asset (resource, sprites)
    [SerializeField] private List<Sprite> m_SpirteEmoteList;

    private void OnDestroy()
    {
        MDEmoteController.Api.OnGetEmoteEvent -= FindEmote;
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;

        MDEmoteController.Api = new MDEmoteController();

        MDEmoteController.Api.OnGetEmoteEvent += FindEmote;
    }

    private void FindEmote(string id, int emoteIndex)
    {
        if (emoteIndex > 0 && emoteIndex <= m_SpirteEmoteList.Count - 1)
        {
            MDEmoteController.Api.SendEmote(id, m_SpirteEmoteList[emoteIndex]);
        }
        else
        {
            Debug.LogError("Emote index out of range");
        }
    }
}