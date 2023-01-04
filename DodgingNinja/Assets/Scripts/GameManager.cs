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
        // oyun ba�lad���nda engeller olu�turuluyor bu sayede oyun s�ras�nda s�rekli bir �eyler spawnlan�p kasmalara sebep olmayacak
        for (int i = 0; i < 10; i++)
        {
            for (int ind = 0; ind < 1; ind++)
            {
                // buras� e�er farkl� toplar ya da engeller spawnlamak istersek diye yap�ld�
                Instantiate(balls[ind], new Vector3(x: 10, y: -0.15f, z: -4.97f), Quaternion.identity);
            }
        }
        // engeller (balls) sahnede aran�p balls dizisine ekleniyor
        balls = FindObjectsOfType<balls>();
        // b�t�n engeller (balls) tek tek deaktif edliyor ihtiyac�m�z oldu�unda aktif edip �a��raca��z
        for (int ind = 0; ind < 10; ind++)
        {
            balls[ind].gameObject.SetActive(false);
        }
        // yar�s�n� sol tarafa g�nderiyoruz
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
            timerText.text = "Kalan S�re: " + countdownTo.ToString("0.##");
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

    public void TekrarOynaButton()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void CikisButton()
    {
        quit
    }

    IEnumerator CallBalls()
    {
        SelectBall();
        float sayi = Random.Range(2.1f, 4.1f);
        yield return new WaitForSeconds(sayi);
        StartCoroutine(CallBalls());
    }
}
