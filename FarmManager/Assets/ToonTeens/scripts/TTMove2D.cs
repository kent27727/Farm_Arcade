using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTMove2D : MonoBehaviour
{

    Animator anim;
    Rigidbody rigid;
    float walk;
    float run;
    int right = 1;
    bool turn;
    //bool crouch;
    //Vector3 look = Vector3.right;
    int n = 0;
    public float jumpforce;
    bool active = true;
    float it = 0f;
    bool grounded;


    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();        
    }


    void Update()
    {
        //GROUNDED
        Debug.DrawRay(transform.position + new Vector3( 0.1f, 0.05f, 0f), new Vector3(0f, -0.1f, 0f), Color.red, 0.0f);
        Debug.DrawRay(transform.position + new Vector3(-0.1f, 0.05f, 0f), new Vector3(0f, -0.1f, 0f), Color.red, 0.0f);

        if (Physics.Raycast(transform.position + new Vector3( 0.1f, 0.05f, 0f), Vector3.down, 0.1f)||
            Physics.Raycast(transform.position + new Vector3(-0.1f, 0.05f, 0f), Vector3.down, 0.1f))
            {
            grounded = true;
            GetComponent<Collider>().material.dynamicFriction = 1;
            GetComponent<Collider>().material.staticFriction = 1;
            anim.SetBool("grounded", true);
            }
        else
            {
            grounded = false;
            GetComponent<Collider>().material.dynamicFriction = 0;
            GetComponent<Collider>().material.staticFriction = 0;
            anim.SetBool("grounded", false);
            }



        if (active)
        { 
        //WALK & run
        walk = Input.GetAxisRaw("Horizontal");
        if (right == -1 && walk <= 0f || right == 1 && walk >= 0f) anim.SetFloat("walk", Mathf.Abs(walk));
        
        if (Input.GetKeyDown("a") && right ==1 && !turn)
            {
            anim.SetTrigger("turn");
            turn = true;
            //right = -1;
            }
        if (Input.GetKeyDown("d") && right == -1 && !turn)
            {
            anim.SetTrigger("turn");
            turn = true;
            //right = 1;
        }

        
        //LOOK & MOVE
        if (turn) Chturn();
        if (grounded) rigid.velocity = (new Vector3(anim.GetFloat("speed")*right, 0f, 0f));

        
        //RUN
        if (Input.GetKey(KeyCode.LeftShift) && run < 60 ) run+=0.75f;
        if (!Input.GetKey(KeyCode.LeftShift) && run > 0 ) run-=0.6f;
        anim.SetFloat("run", run);
        if (walk == 0f) run = Mathf.Lerp (run,0f,0.15f);

        //CROUCH
        if (Input.GetKey("c")) anim.SetBool("crouch", true);
        if (!Input.GetKey("c")) anim.SetBool("crouch", false);
        //else anim.SetBool("crouch", false);

        //JUMP
        if (Input.GetButtonDown("Jump") && grounded && !turn)
            { 
                anim.SetTrigger("jump");                
                StartCoroutine("Inactive", 0.85f);
                if (anim.GetFloat("walk") == 0f) StartCoroutine("Jump"); 
                else rigid.AddForce(Vector3.up * jumpforce + (transform.forward * run * 0.015f), ForceMode.Impulse);
            }
        }

        if (!grounded) rigid.velocity += new Vector3(Input.GetAxisRaw("Horizontal")*0.025f, 0f, 0f);
    }


    void Chturn()
    {
        if (n < 36)
        {
            transform.Rotate(new Vector3(0f, 5f, 0f));
            n++;
        }
        if (n == 18) right *= -1;
        if (n >= 36)
        {
            n = 0;
            turn = false;
            transform.rotation = Quaternion.LookRotation(new Vector3(right, 0f, 0f));
        }
    }


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
        while (it < 0.3f)
        {
            it += Time.deltaTime;
            yield return null;
        }
        rigid.AddForce(Vector3.up * jumpforce + (transform.forward * run) * 0.015f, ForceMode.Impulse);
        it = 0;
        yield return null;
    }

}
