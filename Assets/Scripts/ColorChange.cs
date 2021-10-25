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
    }
    
    public void OnTarget()
    {
        rend.GetPropertyBlock(matPropBlock);
        originalColor = matPropBlock.GetColor("_BaseColor");
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
