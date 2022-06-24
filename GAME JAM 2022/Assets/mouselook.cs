using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouselook : MonoBehaviour
{
    public float Mousesensitivity = 100f;
    public Transform Playerbody;
    float xRotation = 0f;
    float camerashake = 0f;
    float shakeangle = 2f;
    public float speedmultiplier = 2f;
    float speedshake = 5f;
    public GameObject chr;
    public float speedset = 3f;
    public movements chsp;
    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {

        chsp = chr.GetComponent<movements>();

        speedshake = chsp.speed * speedmultiplier / speedset;
        shakeangle = chsp.speed / speedmultiplier;


        float x = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.W))
        {

            camerashake = camerashake + speedshake * Time.deltaTime;

        }
        else
        {
            camerashake = Mathf.PI / 2;

        }

        if (camerashake > 100)
        {
            camerashake = 0f;


        }
        float mouseX = Input.GetAxis("Mouse X") * Mousesensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Mousesensitivity * Time.deltaTime;

        Playerbody.Rotate(Vector3.up * mouseX);

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        xRotation -= mouseY;
        transform.localRotation = Quaternion.Euler(xRotation, 0f, Mathf.Cos(camerashake) * shakeangle);

    }
}