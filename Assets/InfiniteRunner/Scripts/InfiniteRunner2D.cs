﻿using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class InfiniteRunner2D : NetworkBehaviour
{
    
    public float speed;
    //public float sideSpeed;
    
    public float BoostSpeed;
    //public Text distancemoved;
   
    public Rigidbody2D rigid;
   
    public float jumpForce;
    
    public bool isGrounded = false;
    //Vector3 movement;
    
    Vector3 movement1;

    public Animator anim;


   
    // Start is called before the first frame update
    public void Start()
   {
       rigid = GetComponent<Rigidbody2D>();
       // InvokeRepeating("distance", 0, 1 / speed);

        anim = GetComponent<Animator>();


    }

    //public void distance()
    //{
    //distancemoved.text displays as current position on x axis!
    // distancemoved.text = "Player 1 Distance: " + transform.position.x.ToString();
    // }

    
    public void Update()
    {   
        if (GetComponent<NetworkIdentity>().hasAuthority)
            Controls();
      
    }


    public void Controls()
    {

        if (base.isClient && base.hasAuthority)


            transform.localEulerAngles = new Vector2(0, 0);


        //transform.Translate(Vector3.right * speed * Time.deltaTime);

        //Movement with network parameters.
        if (base.isClient && base.hasAuthority)
            //transform.position += movement * Time.deltaTime * sideSpeed;
            transform.position += movement1 * Time.deltaTime * BoostSpeed;
        


        //movement.z = Input.GetAxisRaw("Horizontal") * -1.0f;

        //Jump with network parameters.
        if (base.isClient && base.hasAuthority)
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true || Input.GetKeyDown("joystick button 0") && isGrounded == true)
            {
                jumpForce = 15;

                GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                anim.SetBool("Runner2RUN", false);
                anim.SetBool("Runner2JUMP", true);
                anim.SetBool("Runner2RUNleft", false);


            }
       

        //if up arrow is pushed characters speed increases
        if (base.isClient && base.hasAuthority)
            movement1.x = Input.GetAxisRaw("Horizontal");
        if (Input.GetAxisRaw("Horizontal") > 0)
        {

            anim.SetBool("Runner2RUN", true);
            anim.SetBool("Runner2JUMP", false);
            anim.SetBool("Runner2RUNleft", false);

        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            anim.SetBool("Runner2RUN", false);
            anim.SetBool("Runner2JUMP", false);
            anim.SetBool("Runner2RUNleft", true);
        }
    }
            void OnTriggerStay2D(Collider2D col)
    {
        if (base.isClient && base.hasAuthority)
            if (col.gameObject.tag == "ground")
            {

                isGrounded = true;
            }

    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (base.isClient && base.hasAuthority)
            if (col.gameObject.tag == "ground")
            {
                isGrounded = false;
               
            }

    }
}
        
  




