using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{

    public Transform Player_Object;
    public Transform Player_Camera;


    [Space]

    [Range(1f, 200f)]
    public float Mouse_Speed = 100f;


    float PlayerRotation_Y = 0f;
    void Update()
    {

        Player_MouseControl();

    }

    void Player_MouseControl()
    {
        float xMouse = Input.GetAxis("Mouse X") * Mouse_Speed * Time.deltaTime;
        float yMOuse = Input.GetAxis("Mouse Y") * Mouse_Speed * Time.deltaTime;

        //player rotate horizontal
        Player_Object.Rotate(Vector3.up * xMouse);


        //camera rotate vertical
        PlayerRotation_Y -= yMOuse;
        PlayerRotation_Y = Mathf.Clamp(PlayerRotation_Y, -85f, 85f);
        Quaternion quaternion = Quaternion.Euler(PlayerRotation_Y, 0, 0);
        Player_Camera.localRotation = quaternion;
    }

}