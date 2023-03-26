using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Timer timer;
    public TMPro.TextMeshProUGUI threeStarText;
    public TMPro.TextMeshProUGUI fiveStarText;
    public TMPro.TextMeshProUGUI ayakaText;
    public TMPro.TextMeshProUGUI timeSpentText;
    public TMPro.TextMeshProUGUI primosText;

    void Start()
    {
        gameObject.SetActive(false);
    }
    
    public void LoadMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void OpenScreen(int threeStar, int fiveStar, int ayaka, int primos)
    {
        if (gameObject.activeSelf) {
            return;
        }
        float timeSpent = timer.timeLimit - timer.timeRemaining;
        float minutes = Mathf.FloorToInt(timeSpent / 60);
        float seconds = Mathf.FloorToInt(timeSpent % 60);
        threeStarText.text = threeStar.ToString();
        fiveStarText.text = fiveStar.ToString();
        ayakaText.text = ayaka.ToString();
        timeSpentText.text = string.Format("{0:00} minute(s) {1:00} second(s)", minutes, seconds);
        primosText.text = primos.ToString();
        gameObject.SetActive(true);
    }
}
