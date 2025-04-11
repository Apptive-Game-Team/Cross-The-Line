using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float rotSpeed = 4000f;

    private float mx = 0;
    private float my = 0;

    private Vector3 ScreenCenter;

    private void Start()
    {
        ScreenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }

    void Update()
    {
        float x = Input.GetAxis("Mouse X");    
        float y = Input.GetAxis("Mouse Y");    
        
        mx += x * rotSpeed * Time.deltaTime;
        my -= y * rotSpeed * Time.deltaTime;
        
        mx = Mathf.Clamp(mx, -10f, 10f);
        my = Mathf.Clamp(my, -10f, 10f);
        
        transform.rotation = Quaternion.Euler(my, mx, 0f);
        
        Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
    }
}
