using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public balls[] balls;
    private int selectedBall = -1;
    public RawImage[] hearts;
    public int health = 3;
    public TextMeshProUGUI timerText;
    float countdownTo = 60.0f;
    public GameObject gameOverPanel;
    public GameObject youWinPanel;

    void Start()
    {
        countdownTo = 60.0f;
        Time.timeScale = 1;
        // oyun baþladýðýnda engeller oluþturuluyor bu sayede oyun sýrasýnda sürekli bir þeyler spawnlanýp kasmalara sebep olmayacak
        for (int i = 0; i < 10; i++)
        {
            for (int ind = 0; ind < 1; ind++)
            {
                // burasý eðer farklý toplar ya da engeller spawnlamak istersek diye yapýldý
                Instantiate(balls[ind], new Vector3(x: 10, y: -0.15f, z: -4.97f), Quaternion.identity);
            }
        }
        // engeller (balls) sahnede aranýp balls dizisine ekleniyor
        balls = FindObjectsOfType<balls>();
        // bütün engeller (balls) tek tek deaktif edliyor ihtiyacýmýz olduðunda aktif edip çaðýracaðýz
        for (int ind = 0; ind < 10; ind++)
        {
            balls[ind].gameObject.SetActive(false);
        }
        // yarýsýný sol tarafa gönderiyoruz
        for (int ind = 0; ind < 5; ind++)
        {
            balls[ind].transform.position = new Vector3(x: -10, y: -0.15f, z: -4.97f);
        }
        StartCoroutine(CallBalls());
    }
    private void Update()
    {
        countdownTo -= Time.deltaTime;

        if (countdownTo > 0)
        {
            timerText.text = "Kalan Süre: " + countdownTo.ToString("0.##");
        }
        else if (countdownTo == 0 || countdownTo < 0)
        {
            timerText.text = "Kalan Süre: " + countdownTo.ToString("0.##");
            YouWin();
        }
    }

    public void SelectBall()
    {
        int sayi1 = Random.Range(1, 3);
        int sayi2 = 5;

        for (int i = 0; i < sayi1;)
        {
            int a = Random.Range(0, balls.Length - sayi2);
            if (a != selectedBall)
            {
                selectedBall = a;
                if(sayi1 == 2)
                {
                    sayi2 = 0;
                }
                i++;
                balls[selectedBall].gameObject.SetActive(true);
            }
        }
    }

    public void Hit()
    {
        if (health > 0)
        {
            hearts[health-1].gameObject.SetActive(false);
            health -= 1;
        }
        if (health == 0)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void YouWin()
    {
        youWinPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void TekrarOynaButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        health = 3;
    }
    public void CikisButton()
    {
        Application.Quit();
    }

    IEnumerator CallBalls()
    {
        SelectBall();
        float sayi = Random.Range(2.1f, 4.1f);
        yield return new WaitForSeconds(sayi);
        StartCoroutine(CallBalls());
    }
}
