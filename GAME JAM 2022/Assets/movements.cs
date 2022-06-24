using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movements : MonoBehaviour
{
    public CharacterController CCPerso;
    public Vector3 VMove;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        VMove = Vector3.zero;
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
        CCPerso.Move(VMove*speed*Time.deltaTime);
    }

    
}
