using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    public string tagToCompare = "Player";

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(tagToCompare))
        {
            Collect();
        }
    }
    protected virtual void Collect()
    {
        OnCollect();
    }

    protected virtual void OnCollect()
    {

    }
}
