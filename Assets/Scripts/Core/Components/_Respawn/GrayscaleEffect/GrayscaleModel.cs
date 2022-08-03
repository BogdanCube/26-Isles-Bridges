using System;
using System.Collections;
using System.Collections.Generic;
using Core.Characters.Base;
using Core.Components.GrayscaleEffect;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class GrayscaleModel : MonoBehaviour
{
    [SerializeField] private GrayMaterial _grayMaterial;
    private readonly string _nameID = "_AmbientCol";

    private void Start()
    {
        SetFade(0);
    }

    [Button]
    public Tween FadeGray(float time = 5)
    {
        return DOTween.To(SetFade, 0, 1, time);
    }

    [Button]
    public Tween FadeToDefault(float time = 5)
    {       
        return DOTween.To(SetFade, 1, 0, time);
    }
    private void SetFade(float value)
    {
        _grayMaterial.Material.SetFloat(_nameID, value);
    }
}
