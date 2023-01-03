using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class movement : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;

    //private bool is_jump;
    private bool is_run;
    private static float speed = 3f;
    private float horizontalInput;
    //private float verticalInput;

    void Start()
    {
        // rigidbody ve animatorün tanýmlanmasý
        rb = GetComponent<Rigidbody>();
        anim= GetComponent<Animator>();

        // kodun baþlangýcýnda koþmanýn aktif olmamasý için deðerini false olarak ayarlýyoruz
        is_run = false;
    }

    void FixedUpdate()
    {
        // karakterin hareket kodlarý
        horizontalInput = Input.GetAxis("Horizontal");

        // daha mutlak bir deðer alýp kontrol ediyoruz
        if (Mathf.Abs(horizontalInput) > 0.01f)
        {
            // karakterin hareketi
            transform.rotation = Quaternion.LookRotation(new Vector3(horizontalInput, 0f, 0f));
            rb.MovePosition(rb.position - transform.forward * speed * Time.fixedDeltaTime * -1f);

            // karakterin animasyonu

            if (!is_run)
            {
                is_run = true;
                anim.SetBool("is_run", true);
            }
        }
        else if (is_run)
        {
            is_run = false;
            anim.SetBool("is_run", false);
        }
    }
}