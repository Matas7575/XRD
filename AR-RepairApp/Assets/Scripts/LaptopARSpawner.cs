using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LaptopARSpawner : MonoBehaviour
{
    private ARTrackedImageManager arTrackedImageManager;

    private void Awake()
    {
        arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        arTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
       
        foreach (var trackedImage in eventArgs.added)
        {
            UpdatePrefabTransform(trackedImage);
        }

       
        foreach (var trackedImage in eventArgs.updated)
        {
            UpdatePrefabTransform(trackedImage);
        }
    }

    private void UpdatePrefabTransform(ARTrackedImage trackedImage)
    {
     
        GameObject spawnedPrefab = trackedImage.transform.GetChild(0).gameObject; 
        GameObject spawnedPrefab2 = trackedImage.transform.GetChild(1).gameObject;
        GameObject spawnedPrefab3 = trackedImage.transform.GetChild(2).gameObject;
        GameObject spawnedPrefab4 = trackedImage.transform.GetChild(3).gameObject;


        if (spawnedPrefab != null)
        {
          
            spawnedPrefab.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            spawnedPrefab2.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            spawnedPrefab3.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            spawnedPrefab4.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            
        }
    }
}
