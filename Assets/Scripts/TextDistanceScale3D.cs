using UnityEngine;
using TMPro;


[RequireComponent(typeof(TextMeshPro))]
public class TextDistanceScale3D : MonoBehaviour
{
    [Header("Camera")]
    public Camera targetCamera;               // default: Camera.main
    
    [SerializeField] private float referenceDistance = 10f;
    //besar teks minnya
    [SerializeField] private float minScale = 0.5f;
    //nambah scale text per unit F itu
    [SerializeField] private float scalePerUnit = 0.07f;
    //besar teks maksnya
    [SerializeField] private float maxScale = 10f;

    void Awake()
    {
        if (!targetCamera) targetCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (!targetCamera) return;

        // 1. Billboard
        transform.rotation = targetCamera.transform.rotation;

        // 2. Hitung selisih absolut terhadap referenceDistance
        float distance = Vector3.Distance(transform.position, targetCamera.transform.position);
        float delta    = Mathf.Abs(distance - referenceDistance);

        // 3. Skala = minScale + delta * scalePerUnit
        float scale = minScale + delta * scalePerUnit;

        // 4. Clamp
        if (maxScale > 0f) scale = Mathf.Min(scale, maxScale);

        transform.localScale = Vector3.one * scale;
    }
}
