using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{

    public static CountdownTimer Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    [Header("UI")]
    [SerializeField] private TMP_Text timerText;

    [Header("Timer")]
    [SerializeField] private float startingTime = 60f;

    public float TimeRemaining { get; private set; }

    public bool IsFinished => TimeRemaining <= 0f;

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        if (IsFinished)
            return;

        TimeRemaining -= Time.deltaTime;
        TimeRemaining = Mathf.Max(TimeRemaining, 0f);

        UpdateDisplay();

        if (IsFinished)
        {
            Debug.Log("Time's up!");
            // Trigger game over, victory, etc.
        }
    }

    public void ResetTimer()
    {
        TimeRemaining = startingTime;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        int seconds = Mathf.FloorToInt(TimeRemaining);
        int centiseconds = Mathf.FloorToInt((TimeRemaining - seconds) * 100);

        timerText.text = $"{seconds:00}:{centiseconds:00}";
    }

    public void AddTime(float seconds)
    {
        TimeRemaining += seconds;
        UpdateDisplay();
    }

    public void SetTime(float seconds)
    {
        TimeRemaining = Mathf.Max(0f, seconds);
        UpdateDisplay();
    }
}