using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class movement : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;

    private bool is_jumped = false;
    private bool is_run;
    private static float speed = 3f;
    private static float jumpHeight = 2.2f;
    private float horizontalInput;
    float jumpForce;
    //private float verticalInput;

    void Start()
    {
        // rigidbody ve animatorün tanýmlanmasý
        rb = GetComponent<Rigidbody>();
        anim= GetComponent<Animator>();

        // kodun baþlangýcýnda koþmanýn aktif olmamasý için deðerini false olarak ayarlýyoruz
        is_run = false;
        jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics.gravity.y * rb.mass));
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

        // karakterin zýplamasý
        if (Input.GetKey(KeyCode.Space) && is_jumped == false)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            is_jumped = true;
            anim.SetTrigger("is_jump");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "floor")
        {
            is_jumped = false;
        }
    }
}