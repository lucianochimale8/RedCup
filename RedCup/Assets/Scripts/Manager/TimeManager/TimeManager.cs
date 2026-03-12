using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    private float elapsedTime;
    private bool isRunning;

    public float ElapsedTime => elapsedTime;

    #region Unity Lifecycle
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        if (!isRunning)
            StartTimer();
    }
    private void Update()
    {
        if (!isRunning)
            return;

        elapsedTime += Time.deltaTime;
    }
    #endregion

    #region Modificar el tiempo
    public void StartTimer()
    {
        elapsedTime = 0f;
        isRunning = true;
    }
    public void StopTimer()
    {
        isRunning = false;
    }
    public void ResetTimer()
    {
        elapsedTime = 0f;
        isRunning = false;
    }
    #endregion

    #region Get Time
    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    #endregion
}
