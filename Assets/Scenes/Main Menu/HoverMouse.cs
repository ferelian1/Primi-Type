using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class HoverMouse : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler,
    IPointerDownHandler
{
    [Header("Scale Settings")]
    public Vector3 normalScale   = new Vector3(2f, 5f, 1f);
    public Vector3 hoverScale    = new Vector3(2.5f, 5.5f, 1f);
    public Vector3 clickScale    = new Vector3(1.8f, 4.8f, 1f);  // skala saat klik
    [Tooltip("Kecepatan transisi skalanya")]
    public float   lerpSpeed     = 10f;

    private RectTransform rectTransform;
    private Vector3       targetScale;
    private bool          isPointerOver = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localScale = normalScale;
        targetScale = normalScale;
    }

    void Update()
    {
        rectTransform.localScale = Vector3.Lerp(
            rectTransform.localScale,
            targetScale,
            Time.deltaTime * lerpSpeed
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;
        targetScale = hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;
        targetScale = normalScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        targetScale = clickScale;
    }

}
