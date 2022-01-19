using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonsManager : MonoBehaviour
{
    public List<GameObject> buttons;

    [Header("Animation")]
    public float duration = .2f;
    public float delay = .1f;
    private Vector3 scale;
    public Ease ease = Ease.OutBack;


    private void Awake()
    {
        foreach(var b in buttons)
        {
            scale = b.transform.localScale;
        }
    }

    private void OnEnable()
    {
        HideButton();
        ShowButtons();
    }

    private void HideButton()
    {
        foreach (var b in buttons)
        {
            b.transform.localScale = Vector3.zero;
            b.SetActive(false);
        }
    }

    private void ShowButtons()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            var b = buttons[i];
            b.SetActive(true);
            b.transform.DOScale(scale, duration).SetDelay(i * delay).SetEase(ease);
        }
    }
} 
