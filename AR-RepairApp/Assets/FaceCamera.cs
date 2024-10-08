using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera mainCamera;

    void Update()
    {
        if (mainCamera != null)
        {
            // Make the canvas face the camera
            transform.LookAt(mainCamera.transform);
            transform.Rotate(0, 180, 0); // Rotate to face the camera correctly
        }
    }
}