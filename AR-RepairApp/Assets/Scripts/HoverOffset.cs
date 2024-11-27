using UnityEngine;

public class HoverOffset : MonoBehaviour
{
    public float hoverHeight = 0.1f;

    private Transform parentTransform;

    void Start()
    {
        parentTransform = transform.parent;
    }

    void Update()
    {
        if (parentTransform != null)
        {
            transform.position = parentTransform.position + new Vector3(0, hoverHeight, 0);
            transform.rotation = parentTransform.rotation; 
        }
    }
}
