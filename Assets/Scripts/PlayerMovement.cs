using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [SerializeField] private const float SPEED = 50f;

    [SerializeField] private GameObject playerBase;
    [SerializeField] private State state;
    [SerializeField] private Rigidbody2D playerRigidbody2D;
    [SerializeField] private Vector3 moveDir;

    /////// STATE OPTIONS ///////
    private enum State 
    {
        Normal,
    }

    /////// STATE INITIALIZATION ///////
    private void Awake() 
    {
        instance = this;
        playerBase = gameObject.GetComponent<GameObject>();
        playerRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        SetStateNormal();
    }

    private void Update() 
    {
        switch (state) {
        case State.Normal:
            HandleMovement();
            break;
        }
    }
    
    /////// STATE INITIALIZATION ///////
    private void SetStateNormal() 
    {
        state = State.Normal;
    }

    /////// TRANSLATE INPUT TO VECTOR ///////
    /////// NEEDS CHANGE TO NEW INPUT SYSTEM ////////
    private void HandleMovement() 
    {
        float moveX = 0f;
        float moveY = 0f;
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            moveX = +1f;
        }
        
        moveDir = new Vector3(moveX, moveY).normalized;
    }

    /////// TRANSLATE INPUT TO VECTOR ///////
    private void FixedUpdate() 
    {
        bool isIdle = moveDir.x == 0 && moveDir.y == 0;
        if (isIdle) {
            //playerBase.PlayIdleAnim();
        } else {
            //playerBase.PlayMoveAnim(moveDir);
            //transform.position += moveDir * SPEED * Time.deltaTime;
            playerRigidbody2D.MovePosition(transform.position + moveDir * SPEED * Time.fixedDeltaTime);
        }
        
    }
}
