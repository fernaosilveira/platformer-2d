using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectableCoin : CollectableBase
{
    [Header("Animation Params")]
    public float animationDuration;
    public Vector2 endScale;
    public bool canCollect = true;

    protected override void OnCollect()
    {
        if (canCollect)
        {
            base.OnCollect();
            CollectableManager.Instance.AddCoins();
            canCollect = false;
        }

        gameObject.transform.DOScale(0, animationDuration);
        Invoke("DestroyObject", animationDuration);
    }

    protected virtual void DestroyObject()
    {
        Destroy(gameObject);
    }
}
