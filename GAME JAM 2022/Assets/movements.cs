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
    public float fall = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //gravite

        
        if (Physics.CheckSphere(sol.position, 0.2f, ground))
        {
            fall = 0;
        }
        else
        {
            fall = (-2.5f* Time.deltaTime + VMove.y);
            
        }
        VMove = new Vector3(0,fall,0);
        //fin


        if (Input.GetKey(KeyCode.W))
        {
            VMove = VMove + transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            VMove = VMove - transform.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            VMove = VMove - transform.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            VMove = VMove + transform.right;
        }



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

        CCPerso.Move(VMove*speed*Time.deltaTime);
    }

    
}
