using System;
using UnityEngine;

public class ColorChange : MonoBehaviour, ITargetable
{
    [SerializeField] private Color colorWhenTargeted;

    private Color originalColor;
    private Renderer rend;
    private MaterialPropertyBlock matPropBlock;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        matPropBlock = new MaterialPropertyBlock();
        originalColor = rend.material.GetColor("_BaseColor");
    }

    public void OnTarget()
    {
        rend.GetPropertyBlock(matPropBlock);
        matPropBlock.SetColor("_BaseColor", colorWhenTargeted);
        rend.SetPropertyBlock(matPropBlock);
    }

    public void OnDeTarget()
    {
        rend.GetPropertyBlock(matPropBlock);
        matPropBlock.SetColor("_BaseColor", originalColor);
        rend.SetPropertyBlock(matPropBlock);
    }
}
