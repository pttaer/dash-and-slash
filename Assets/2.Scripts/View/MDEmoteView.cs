using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MDEmoteView : MonoBehaviour
{
    private void OnEnable()
    {
        transform.localScale = new Vector3(1,0,1);
        transform.DOScaleY(1, 0.3f);
    }
}