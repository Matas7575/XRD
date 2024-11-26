using UnityEngine;

public class ModularBlinkingEffect : MonoBehaviour
{
    public Transform parent; 
    public Color blinkColor = Color.red; 
    public float blinkInterval = 0.5f; 

    private Renderer[] allRenderers; 
    private Color[] originalColors;

    void Start()
    {
        if (parent == null)
        {
            parent = transform;
        }

        allRenderers = parent.GetComponentsInChildren<Renderer>(includeInactive: true);
        originalColors = new Color[allRenderers.Length];

        for (int i = 0; i < allRenderers.Length; i++)
        {
            originalColors[i] = allRenderers[i].material.color;
        }

        InvokeRepeating(nameof(Blink), 0f, blinkInterval);
    }

    void OnDestroy()
    {
        CancelInvoke(nameof(Blink));
    }

    private void Blink()
    {
        for (int i = 0; i < allRenderers.Length; i++)
        {
            if (allRenderers[i] != null)
            {
                allRenderers[i].material.color = 
                    allRenderers[i].material.color == originalColors[i] ? blinkColor : originalColors[i];
            }
        }
    }
}
