using UnityEngine;
using TMPro; // ใช้ TextMeshPro สำหรับ UI

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 60f; // เวลานับถอยหลังเริ่มต้น
    private float currentTime;
    public TextMeshProUGUI timerText; // UI แสดงเวลา

    private bool isCounting = false;

    public GameManagerScript gameManager; // 🔥 เพิ่มตัวแปร GameManager

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
            timerText.text = Mathf.Max(Mathf.Ceil(currentTime), 0).ToString(); // ป้องกันค่าติดลบ
        }
    }

    void OnTimeUp()
    {
        Debug.Log("⏳ Time's up! Game Over!");

        if (gameManager != null)
        {
            gameManager.gameOver(); // 🔥 เรียก Game Over
        }
        else
        {
            Debug.LogError("GameManager ยังไม่ได้ถูกกำหนดใน Inspector!");
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
