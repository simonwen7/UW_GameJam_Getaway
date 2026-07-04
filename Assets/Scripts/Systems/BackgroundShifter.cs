using UnityEngine;

public class BackgroundShifter : MonoBehaviour
{
    [Header("Transition Settings")]
    [SerializeField]
    private Vector3 targetPos;
    [SerializeField]
    private float transitionSpeed;

    [HideInInspector]
    public static bool isTransition = false;

    void Awake()
    {
        isTransition = false;
    }

    void Update()
    {
        if (!isTransition) return;

        if (transform.position.y != targetPos.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * transitionSpeed);
        }
    }
}
