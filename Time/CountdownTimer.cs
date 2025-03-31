using UnityEngine;
using TMPro; //Use TextMeshPro for UI

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 60f; //Countdown timer starts.
    private float currentTime;
    public TextMeshProUGUI timerText; //UI displays the time.
    private bool isCounting = false;
    public GameManagerScript gameManager; //Add GameManager variables.

    void Start()
    {
        StartCountdown(countdownTime);
    }

    void Update()
    {
        if (isCounting && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerDisplay();

            if (currentTime <= 0)
            {
                currentTime = 0;
                isCounting = false;
                OnTimeUp();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.Max(Mathf.Ceil(currentTime), 0).ToString(); //Prevent negative values
        }
    }

    void OnTimeUp()
    {
        Debug.Log("⏳ Time's up! Game Over!");

        if (gameManager != null)
        {
            gameManager.gameOver();
        }
    }

    public void StartCountdown(float time)
    {
        countdownTime = time;
        currentTime = countdownTime;
        isCounting = true;
        UpdateTimerDisplay();
    }

    public void Pause()
    {
        isCounting = false;
    }

    public void ResetTimer()
    {
        currentTime = countdownTime;
        isCounting = true;
        UpdateTimerDisplay();
    }
}
