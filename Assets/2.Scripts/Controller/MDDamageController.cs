using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MDDamageController : MonoBehaviour
{
    public static MDDamageController Api { get; internal set; }

    public Action<int> OnDamageToPlayerEvent { get; set;}

    public void DamagePlayer(int damage)
    {
        OnDamageToPlayerEvent?.Invoke(damage);
    }

    public void ShowDamagedColor(SpriteRenderer sr, Action callback = null)
    {
        float shakeDuration = 0.1f;

        Vector3 originalPosition = sr.transform.localPosition;

        sr.DOColor(Color.red, shakeDuration)
          .SetEase(Ease.InOutExpo)
          .OnComplete(() =>
          {
              sr.DOColor(Color.white, shakeDuration).SetEase(Ease.InOutExpo).OnComplete(() =>
              {
                  callback?.Invoke();
              });
          });

        float shakeMagnitude = 0.1f;

        sr.transform.DOShakePosition(shakeDuration, shakeMagnitude)
          .SetEase(Ease.InOutExpo)
          .OnComplete(() => sr.transform.localPosition = originalPosition);
    }
}
