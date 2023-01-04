using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class balls : MonoBehaviour
{
    Rigidbody rb;
    int ballDirection;
    public float ballSpeed = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DestroyBall());

        if (rb.position.x < 0)
        {
            ballDirection = 1;
        }
        else if (rb.position.x > 0)
        {
            ballDirection = -1;
        }
    }
    IEnumerator DestroyBall()
    {
        yield return new WaitForSeconds(6);

        transform.position = new Vector3(x: 26, y: -0.15f, z: -4.97f);
        gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        float position_x = rb.position.x + ballDirection/ballSpeed;
        transform.position = new Vector3(position_x, rb.position.y, rb.position.z);
    }
}
