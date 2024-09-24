using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTrackingHandler : MonoBehaviour
{
    [SerializeField]
    private ARTrackedImageManager trackedImageManager;

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            // Adjust the prefab's rotation when the QR code is detected
            trackedImage.transform.rotation = Quaternion.Euler(-180, 0, 0);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            // Optionally, adjust rotation during update too
            trackedImage.transform.rotation = Quaternion.Euler(-180, 0, 0);
        }
    }
}
