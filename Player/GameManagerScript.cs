using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ✅ ใช้สำหรับ Restart เกม

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    private bool isGameOver = false;

    void Start()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false); // ✅ ซ่อน Game Over UI ตอนเริ่มเกม
        }
    }

    public void gameOver()
    {
        if (!isGameOver) // ✅ กันไม่ให้เรียก Game Over ซ้ำ
        {
            isGameOver = true;
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true);
            }
            else
            {
                Debug.LogError("⚠️ Game Over UI ยังไม่ได้กำหนดใน Inspector!");
            }
            ControlData.Score = 0; //รีเซ็ตคะแนนก่อนเริ่มใหม่
        }
    }

    public void RestartGame()
    {
        ControlData.Score = 0; //รีเซ็ตคะแนนก่อนเริ่มใหม่
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ✅ รีโหลดฉากปัจจุบัน
    }

    public void QuitGame()
    {
        Debug.Log("🚪 Quit Game!");
        Application.Quit(); // ✅ ใช้ออกจากเกม (ใช้ได้กับ Build จริง, ใน Editor ต้องกด Stop เอง)
    }
}
