using System;
using UnityEngine;

public class MDEmoteController
{
    public static MDEmoteController Api;

    public Action<string, Sprite> OnShowEmoteEvent;
    public Action<string, int> OnGetEmoteEvent;

    public void SendEmote(string npcId, Sprite sprite)
    {
        OnShowEmoteEvent?.Invoke(npcId, sprite);
    }

    public void GetEmote(string npcId, int emoteOrder)
    {
        OnGetEmoteEvent?.Invoke(npcId, emoteOrder);
    }

    public void TweenEmote()
    {
        //DOTween.
    }
}