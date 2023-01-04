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
                Instantiate(balls[ind], new Vector3(x: 26, y: -0.45f, z: -4.97f), Quaternion.identity);
            }
        }
        // engeller (balls) sahnede aran�p balls dizisine ekleniyor
        balls = FindObjectsOfType<balls>();
        // b�t�n engeller (balls) tek tek deaktif edliyor ihtiyac�m�z oldu�unda aktif edip �a��raca��z
        for (int ind = 0; ind < 10; ind++)
        {
            balls[ind].gameObject.SetActive(false);
        }
    }

    public void SelectBall()
    {
        for (int i = 0; i < 1;)
        {
            int a = Random.Range(0, balls.Length);
            if (a != selectedBall)
            {
                selectedBall = a;
                i++;
            }
        }
    }

    IEnumerator CallBalls()
    {
        yield return new WaitForSeconds(0);
    }


}
