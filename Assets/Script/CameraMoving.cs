using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{    
    Transform playerTransForm;
    Vector3 Offset;
    void Awake()
    {
        playerTransForm = GameObject.FindGameObjectWithTag("Player").transform;
        Offset = transform.position - playerTransForm.position;
    }

    void LateUpdate()
    {
        transform.position = playerTransForm.position + Offset;
    }
}
