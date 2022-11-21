using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTMove3D : MonoBehaviour
{
    Rigidbody rigidbo;
    Animator anim;
    Transform trans;
    bool grounded;
    float largo;
    Vector3 forcedir;
    Vector3 forcedir2;
    Vector3 forcedir3;
    RaycastHit hit;
    RaycastHit hit2;
    float run;
    Collider coll;
    bool active = true;
    float it = 0f;
    public float jumpforce;


    void Start()
    {
        rigidbo = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        trans = GetComponent<Transform>();
        coll = GetComponent<Collider>();
        largo = 0.085f;
        run = 0;
    }

    void Update()
    {

        //GROUNDED
        Debug.DrawRay(transform.position + new Vector3(0.12f, 0.05f, 0f), new Vector3(0f, -largo, 0f), Color.red, 0.0f);
        Debug.DrawRay(transform.position + new Vector3(0.05f, 0.05f, 0f), new Vector3(0f, -largo, 0f), Color.red, 0.0f);
        Debug.DrawRay(transform.position + new Vector3(-0.05f, 0.05f, 0f), new Vector3(0f, -largo, 0f), Color.red, 0.0f);
        Debug.DrawRay(transform.position + new Vector3(-0.12f, 0.05f, 0f), new Vector3(0f, -largo, 0f), Color.red, 0.0f);

        if (Physics.Raycast(transform.position + new Vector3(0.12f, 0.05f, 0f), Vector3.down, largo)
          || Physics.Raycast(transform.position + new Vector3(0.05f, 0.51f, 0f), Vector3.down, largo)
          || Physics.Raycast(transform.position + new Vector3(-0.05f, 0.05f, 0f), Vector3.down, largo)
          || Physics.Raycast(transform.position + new Vector3(-0.12f, 0.05f, 0f), Vector3.down, largo))
        {
            grounded = true;
            coll.material.dynamicFriction = 1;
            coll.material.staticFriction = 1;
            anim.SetBool("grounded", true);
        }
        else
        {
            grounded = false;
            coll.material.dynamicFriction = 0;
            coll.material.staticFriction = 0;
            anim.SetBool("grounded", false);
        }

        if (active)
        {
            //RUN
            if (Input.GetKey(KeyCode.LeftShift) && run < 100) run ++;
            if (!Input.GetKey(KeyCode.LeftShift) && run > 0) run --;
            if (anim.GetFloat("walk") == 0f) run = Mathf.Lerp(run, 0f, 0.125f);
            anim.SetFloat("run", run);


            //MOVE    
            anim.SetFloat("walk", Input.GetAxisRaw("Vertical"));        
            
            //TURN
            anim.SetFloat("turn", Input.GetAxis("Horizontal"));
            trans.Rotate(0f, anim.GetFloat("angle"), 0f);


            /*// SET THE translation DIRECTION
            Debug.DrawRay(trans.position + (trans.forward * 0.15f) + new Vector3(0f, 0.05f, 0f), Vector3.down, Color.magenta);
            Debug.DrawRay(trans.position + (trans.forward * -0.05f) + new Vector3(0f, 0.05f, 0f), Vector3.down, Color.magenta);


            if (Physics.Raycast(trans.position + (trans.forward * 0.15f) + new Vector3(0f, 0.1f, 0f), Vector3.down, out hit))
            {
                forcedir3 = -Vector3.Cross(Vector3.Cross(hit.normal, trans.forward), hit.normal);
                forcedir3.Normalize();
            }
            else forcedir3 = trans.forward;
            if (Physics.Raycast(trans.position + (trans.forward * -0.05f) + new Vector3(0f, 0.1f, 0f), Vector3.down, out hit))
            {
                forcedir2 = -Vector3.Cross(Vector3.Cross(hit.normal, trans.forward), hit.normal);
                forcedir2.Normalize();
            }
            else forcedir3 = trans.forward;
            forcedir = (forcedir3 + forcedir2);
            forcedir.Normalize();
            forcedir = Quaternion.Euler(0f, anim.GetFloat("angle"), 0f) * forcedir;


            Debug.DrawRay(trans.position + new Vector3(0f, 1f, 0f), -forcedir, Color.green);*/


            // APLY THE SPEED
            forcedir = -trans.forward;
            if (grounded) rigidbo.velocity = forcedir * anim.GetFloat("speed") * -1f;
            //fall faster
            if (rigidbo.velocity.y < 0) rigidbo.velocity += Vector3.up* -2 * Time.deltaTime;
            //going up
            //if (rigidbo.velocity.y > 0) rigidbo.velocity += Vector3.up * 2 * Time.deltaTime;

            //JUMP
            if (Input.GetButtonDown("Jump") && grounded)
            {
                if (anim.GetFloat("walk") <= 0f)
                {
                    StartCoroutine("Jump");                    
                    anim.Play("jump");
                }

                if (anim.GetFloat("walk") > 0f)
                {
                    //StartCoroutine("Jump", (7f));
                    rigidbo.AddForce(Vector3.up * jumpforce + (trans.forward * anim.GetFloat("run") * 0.025f), ForceMode.Impulse);
                    anim.Play("runjumpin");
                }
            }

            //SITDOWN        
            if (anim.GetBool("sit") == true && Input.anyKeyDown) anim.SetBool("sit", false);
            if (Input.GetKeyDown(KeyCode.C) && grounded == true && run == 0f)
            {
                if (Physics.Raycast(trans.position + new Vector3(0.0f, 0.3f, 0f) - trans.forward * 0.1f, -trans.forward, 0.17f)
                    && !Physics.Raycast(trans.position + new Vector3(0.0f, 0.42f, 0f) - trans.forward * 0.1f, -trans.forward, 0.4f))
                {
                    anim.SetBool("sit", true);
                }
                else if (anim.GetFloat("walk") == 0f)
                {
                    anim.Play("lookback");
                }
            }

        //AIR MOVE
        if (!grounded  && Input.GetKey("w")) rigidbo.AddForce(trans.forward * 0.25f, ForceMode.Impulse);
        if (!grounded  && Input.GetKey("s")) rigidbo.AddForce(trans.forward * -0.25f, ForceMode.Impulse);
        if (!grounded) rigidbo.AddForce(Vector3.up * -0.25f, ForceMode.Impulse);
        }


        //HITS    

        //Debug.DrawRay(transform.position + new Vector3(-0.2f, 0.35f, 0f), trans.forward, Color.green, 0.0f);
        Debug.DrawRay(transform.position + trans.up * 0.35f + trans.right * -0.1f, trans.forward, Color.green, 0.0f);
        Debug.DrawRay(transform.position + trans.up * 0.35f + trans.right *  0.1f, trans.forward, Color.green, 0.0f);
        Debug.DrawRay(transform.position + trans.up + trans.right * -0.1f, trans.forward, Color.green, 0.0f);
        Debug.DrawRay(transform.position + trans.up + trans.right * 0.1f, trans.forward, Color.green, 0.0f);



        if (Physics.Raycast(trans.position + trans.up * 0.35f + trans.right * -0.1f, trans.forward, out hit2, 0.33f)
             || Physics.Raycast(trans.position + trans.up * 0.35f + trans.right * 0.1f, trans.forward, out hit2, 0.33f)
             || Physics.Raycast(trans.position + trans.up + trans.right * -0.1f, trans.forward, out hit2, 0.33f)
             || Physics.Raycast(trans.position + trans.up + trans.right * 0.1f, trans.forward, out hit2, 0.33f))
        {
            if (hit2.transform.tag == "damage")
            {
                StartCoroutine("Inactive",(4.5f));
                rigidbo.AddForce(trans.forward * -15f + trans.up , ForceMode.Impulse);
                anim.Play("fall1");
            }
        }
        if (Physics.Raycast(trans.position + trans.up * 0.35f + trans.right * -0.1f, -trans.forward, out hit2, 0.33f)
             || Physics.Raycast(trans.position + trans.up * 0.35f + trans.right * 0.1f, -trans.forward, out hit2, 0.33f)
             || Physics.Raycast(trans.position + trans.up + trans.right * -0.1f, -trans.forward, out hit2, 0.33f)
             || Physics.Raycast(trans.position + trans.up + trans.right * 0.1f, -trans.forward, out hit2, 0.33f))
        {
            if (hit2.transform.tag == "damage")
            {
                StartCoroutine("Inactive", (4.5f));
                rigidbo.AddForce(trans.forward * 15f + trans.up , ForceMode.Impulse);
                anim.Play("fall2");
            }
        }


        if (Input.GetKeyDown(KeyCode.P))
        {
            Application.Quit();            
        }



    }

    /*
    //DAMAGE
    void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.tag == "damage")
            {
                anim.Play("sitdown");
            }
        }
        */


    


    IEnumerator Inactive(float itime)
    {
        while (it < itime)
            {
            active = false;
            it += Time.deltaTime;
            yield return null;
            }
        active = true;
        it = 0;
        yield return null;
    }





    IEnumerator Jump()
    {
        while (it < 0.2f)
        {            
            it += Time.deltaTime;
            yield return null;
        }        
        rigidbo.AddForce(Vector3.up * jumpforce + (trans.forward * anim.GetFloat("run") * 0.125f), ForceMode.Impulse);
        it = 0;
        yield return null;
    }



    
}