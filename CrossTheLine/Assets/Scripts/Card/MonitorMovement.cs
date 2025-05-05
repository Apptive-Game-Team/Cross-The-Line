using System;
using UnityEngine;

public class MonitorMovement : MonoBehaviour
{
    public GameObject monitorGlitter;

    private void Start()
    {
        monitorGlitter = transform.Find("MonitorGlitter").gameObject;
        monitorGlitter.SetActive(false);
    }

    private void OnMouseEnter()
    {
        Debug.Log("Mouse enter");
        monitorGlitter.SetActive(true);
    }

    private void OnMouseExit()
    {
        monitorGlitter.SetActive(false); 
    }
}
