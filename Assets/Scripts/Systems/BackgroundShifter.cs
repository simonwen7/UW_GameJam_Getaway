using UnityEngine;

public class BackgroundShifter : MonoBehaviour
{
    [Header("Background Loop Settings")]
    [SerializeField]
    private GameObject[] cityBackgrounds;
    [SerializeField]
    private GameObject[] forestBackgrounds;
    [SerializeField]
    private float loopSpeed;
    [SerializeField]
    private float loopThreshold;
    [SerializeField]
    private float resetOffset;

    [HideInInspector]
    public static bool isTransition = false;

    private GameObject[] currentBackgrounds;
    private bool transitionDone = false;
    private bool transitionStarted  = false;
    private float pixelOverlapBuffer = 0.02f;

    void Awake()
    {
        isTransition = false;
        currentBackgrounds = cityBackgrounds;
    }

    void Update()
    {
        if (GameManager.Instance.isLevelCompleted) return;

        if (!isTransition) 
        {
            Scroll(currentBackgrounds, loopSpeed);
            transitionDone = false;
            transitionStarted = false;
        } else
        {
            Transition();    
        }
        
    }

    void Scroll(GameObject[] backgrounds, float speed)
    {
        foreach (GameObject background in backgrounds)
        {
            background.transform.Translate(Vector3.down * speed * Time.deltaTime, Space.Self);

            if (background.transform.position.y <= loopThreshold)
            {
                float overshoot = background.transform.position.y - loopThreshold;

                float newY = loopThreshold + resetOffset + overshoot - pixelOverlapBuffer;

                background.transform.position = new Vector3(background.transform.position.x, newY, background.transform.position.z);
            }
        }
    }

    void Transition()
    {
        if (!transitionStarted)
        {
            InitializeForestPositions();
        }

        foreach (GameObject city in cityBackgrounds)
        {
            city.transform.Translate(Vector3.down * loopSpeed * Time.deltaTime);
        }

        Scroll(forestBackgrounds, loopSpeed);

        if (forestBackgrounds[0].transform.position.y <= 0f && !transitionDone)
        {
            isTransition = false;
            transitionDone = true;
            currentBackgrounds = forestBackgrounds; 

            foreach (GameObject city in cityBackgrounds)
            {
                city.SetActive(false);
            }
        }
    }

    void InitializeForestPositions()
    {
        transitionStarted = true;

        float highestCityY = float.MinValue;
        foreach (GameObject city in cityBackgrounds)
        {
            if (city.transform.position.y > highestCityY)
            {
                highestCityY = city.transform.position.y;
            }
        }

        float individualBackgroundHeight = 16f;

        for (int i = 0; i < forestBackgrounds.Length; i++)
        {
            forestBackgrounds[i].transform.position = new Vector3(
                forestBackgrounds[i].transform.position.x, 
                highestCityY + (individualBackgroundHeight * (i + 1)) - pixelOverlapBuffer, 
                forestBackgrounds[i].transform.position.z
            );
        }
    }
}
