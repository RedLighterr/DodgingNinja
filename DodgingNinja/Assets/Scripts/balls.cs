using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balls : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyBall());
    }
    IEnumerator DestroyBall()
    {
        yield return new WaitForSeconds(10);
        this.gameObject.SetActive(false);
    }
}
