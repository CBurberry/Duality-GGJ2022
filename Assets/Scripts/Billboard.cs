using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private float moveUpSpeed = 1f;


    void LateUpdate ()
    {
        //// GET CAMERA POS /////
        Vector3 camPos = Camera.main.transform.position;
        
        //// MOVE SPRITE TO FACE CAMERA /////
        transform.LookAt(
            new Vector3(-camPos.x, transform.position.y, -camPos.z) + Camera.main.transform.forward
            );
        
        //add relative rotation <---
    }
}
