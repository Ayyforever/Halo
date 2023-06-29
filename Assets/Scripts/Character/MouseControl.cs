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
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {

        Player_MouseControl();

    }

    void Player_MouseControl()
    {
        float xMouse = Input.GetAxis("Mouse X") * Mouse_Speed * 0.0167f;
        float yMOuse = Input.GetAxis("Mouse Y") * Mouse_Speed * 0.0167f;

        //player rotate horizontal
        Player_Object.Rotate(Vector3.up * xMouse);


        //camera rotate vertical
        PlayerRotation_Y -= yMOuse;
        PlayerRotation_Y = Mathf.Clamp(PlayerRotation_Y, -85f, 85f);
        Quaternion quaternion = Quaternion.Euler(PlayerRotation_Y, 0, 0);
        Player_Camera.localRotation = quaternion;
    }
    public void Reload_MouseControl(float yMouse)
    {
        PlayerRotation_Y = Mathf.Lerp(PlayerRotation_Y, PlayerRotation_Y + yMouse, 24f * Time.deltaTime);
        PlayerRotation_Y = Mathf.Clamp(PlayerRotation_Y, -85f, 85f);
        Quaternion quaternion = Quaternion.Euler(PlayerRotation_Y, 0, 0);
        Player_Camera.localRotation = quaternion;
    }
    public void Recoil_MouseControl(float xMouse, float yMouse)
    {
        //player rotate horizontal
        Player_Object.Rotate(Vector3.up * xMouse);

        //camera rotate vertical
        PlayerRotation_Y -= yMouse;
        PlayerRotation_Y = Mathf.Clamp(PlayerRotation_Y, -85f, 85f);
        Quaternion quaternion = Quaternion.Euler(PlayerRotation_Y, 0, 0);
        Player_Camera.localRotation = quaternion;
    }
    public void GetDamage(float yMouse)
    {
        PlayerRotation_Y = Mathf.Lerp(PlayerRotation_Y, PlayerRotation_Y + yMouse, 30f * Time.deltaTime);
        PlayerRotation_Y = Mathf.Clamp(PlayerRotation_Y, -85f, 85f);
        Quaternion quaternion = Quaternion.Euler(PlayerRotation_Y, 0, 0);
        Player_Camera.localRotation = quaternion;
    }
}