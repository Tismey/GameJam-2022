using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movements : MonoBehaviour
{
    public CharacterController CCPerso;
    public Vector3 VMove;
    public float speed;
    public float sprint;
    public float cspeed;
    public LayerMask ground;
    public Transform sol;
    public float jumpForce;
    public float fall = 0;
    const float debout = 1;
    const float acroupie = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        VMove = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

        //gravite

        
        if (Physics.CheckSphere(sol.position, 0.2f, ground))
        {
            fall = 0;
            //Jump
            VMove = new Vector3(0, fall, 0);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                fall = jumpForce;
            }
            if (Input.GetKey(KeyCode.W))
            {
                VMove +=  transform.forward;
            }
            if (Input.GetKey(KeyCode.A))
            {
                VMove +=  - transform.right;
            }
            if (Input.GetKey(KeyCode.S))
            {
                VMove += - transform.forward;
            }
            if (Input.GetKey(KeyCode.D))
            {
                VMove += transform.right;
            }
        }
        else
        {
            fall = (-2.5f* Time.deltaTime + VMove.y);
            
        }
        VMove = new Vector3(VMove.x,fall,VMove.z);
        //fin

       

        



        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (speed < sprint)
            {
                speed += (sprint * 0.01f);
            }
        }
        else
        {
            if (speed > cspeed)
            {
                speed -= (sprint * 0.015f);
            }
            else
            {
                speed = cspeed;
            }
        }
        
        //Counch

        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(1, acroupie, 1);
            speed = 5;
        }
        else
        {
            transform.localScale = new Vector3(1, debout, 1);
        }
        //fin

        CCPerso.Move(VMove*speed*Time.deltaTime);
    }

    
}
