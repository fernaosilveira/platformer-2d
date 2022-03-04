using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectableCoin : CollectableBase
{
    [Header("Animation Params")]
    public float animationDuration;
    public Vector2 endScale;

    protected override void OnCollect()
    {
        base.OnCollect();
        CollectableManager.Instance.AddCoins();
        gameObject.transform.DOScale(0, animationDuration);
        if(gameObject.transform.localScale.x == 0)
        {
            Destroy(gameObject);
        }
    }
}
