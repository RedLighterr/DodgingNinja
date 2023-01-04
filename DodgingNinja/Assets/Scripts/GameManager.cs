using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public balls[] balls;
    private int selectedBall = -1;

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

    public void SelectBall()
    {
        int sayi1 = Random.Range(1, 3);
        print(sayi1);
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
        print(sayi2);
    }

    IEnumerator CallBalls()
    {
        SelectBall();
        float sayi = Random.Range(2.1f, 4.1f);
        yield return new WaitForSeconds(sayi);
        StartCoroutine(CallBalls());
    }


}
