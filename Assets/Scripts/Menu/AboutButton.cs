using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AboutButton : MonoBehaviour
{
    public GameObject aboutScreen;
    private bool showScreen = false;

    [Header("Animation")]
    public float duration = .3f;
    public Ease ease = Ease.OutBack;

    public void ShowMenu()
    {
        if(showScreen == false)
        {
            aboutScreen.SetActive(true);
            aboutScreen.transform.DOScale(0, duration).SetEase(ease).From();
            showScreen = true;
        }
        else 
        {
            showScreen = false;
            aboutScreen.SetActive(false);
        }
    }
}
