using UnityEngine;

public class BlinkingEffect : MonoBehaviour
{
    public Transform parent; // The parent whose children will blink
    public Color blinkColor = Color.red; // Color to blink to
    public float blinkInterval = 0.5f; // Time between color switches

    private bool isBlinking = false;
    private Renderer[] childRenderers;
    private Color[] originalColors;

    void Start()
    {
        if (parent == null)
        {
            parent = transform;
        }

        childRenderers = parent.GetComponentsInChildren<Renderer>();
        originalColors = new Color[childRenderers.Length];

        for (int i = 0; i < childRenderers.Length; i++)
        {
            originalColors[i] = childRenderers[i].material.color;
        }
    }

    public void StartBlinking()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            InvokeRepeating(nameof(Blink), 0f, blinkInterval);
        }
    }

    public void StopBlinking()
    {
        if (isBlinking)
        {
            isBlinking = false;
            CancelInvoke(nameof(Blink));

            // Reset all child renderers to their original colors
            for (int i = 0; i < childRenderers.Length; i++)
            {
                if (childRenderers[i] != null)
                {
                    childRenderers[i].material.color = originalColors[i];
                }
            }
        }
    }

    private void Blink()
    {
        for (int i = 0; i < childRenderers.Length; i++)
        {
            if (childRenderers[i] != null)
            {
                childRenderers[i].material.color = 
                    childRenderers[i].material.color == originalColors[i] ? blinkColor : originalColors[i];
            }
        }
    }
}
