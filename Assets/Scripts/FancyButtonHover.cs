using UnityEngine;
using UnityEngine.EventSystems;

public class FancyButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float hoverScale = 1.06f;
    [SerializeField] private float pressedScale = 0.96f;
    [SerializeField] private float speed = 12f;

    private RectTransform rectTransform;
    private Vector3 targetScale;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        targetScale = Vector3.one;
    }

    private void Update()
    {
        rectTransform.localScale = Vector3.Lerp(
            rectTransform.localScale,
            targetScale,
            Time.unscaledDeltaTime * speed
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = Vector3.one * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = Vector3.one;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        targetScale = Vector3.one * pressedScale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        targetScale = Vector3.one * hoverScale;
    }
}
