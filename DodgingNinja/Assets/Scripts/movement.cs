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
    //private static float speed = 10f;
    private static float jumpHeight = 2.2f;
    private float horizontalInput;
    float jumpForce;
    //private float verticalInput;

    float posX = 0;
    float min = -6.4f, max = 6.4f;


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
        // karakterin hareketi ve animasyonlarý
        horizontalInput = Input.GetAxis("Horizontal");

        // daha mutlak deðer alýp kontrol ediyoruz böylece eksi yada artý için ayrý if bloðu kullanmýyoruz
        if (Mathf.Abs(horizontalInput) > 0.01f)
        {
            hareket();
            // karakterin hareketi
            transform.rotation = Quaternion.LookRotation(new Vector3(horizontalInput, 0f, 0f));

            // karakterin animasyonu
            // eðer koþmuyorsa koþmayý aktifleþtirip animasyonu aktif ediyoruz
            if (!is_run)
            {
                is_run = true;
                anim.SetBool("is_run", true);
            }
        }
        // eðer hareket etmiyorsa koþmayý deaktif ediyoruz ve animasyonu kapatýyoruz
        else if (is_run)
        {
            is_run = false;
            anim.SetBool("is_run", false);
        }

        // karakterin zýplamasý ve animasyonu
        if (Input.GetKey(KeyCode.Space) && is_jumped == false)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            is_jumped = true;
            anim.SetTrigger("is_jump");
        }
    }
    void hareket()
    {
        posX += Input.GetAxis("Horizontal")/10;
        //posZ += Input.GetAxis("Vertical");

        posX = Mathf.Clamp(posX, min, max);
        transform.position = new Vector3(posX, rb.position.y, rb.position.z);
    }

    // karakterin zýpladýktan sonra yere basana kadar tekrar zýplamasýný engellemek için yere temas ettiðinde zýplayabilmesini tekrar aktif ediyoruz
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "floor")
        {
            is_jumped = false;
        }
    }
}