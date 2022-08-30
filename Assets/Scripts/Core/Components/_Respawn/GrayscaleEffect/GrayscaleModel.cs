using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class GrayscaleModel : MonoBehaviour
{
    [SerializeField] private List<Renderer> _renderers;
    private readonly string _nameID = "_AmbientCol";
    
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
        foreach (var material in _renderers.SelectMany(renderer => renderer.materials))
        {
            material.SetFloat(_nameID, value);
        }
    }
}
