using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [SerializeField] private const float SPEED = 5f;

    private GameObject playerBase;
    [SerializeField] private State state;
    private Rigidbody2D playerRigidbody2D;
    private InputActions inputActions;

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
        state = State.Normal;   //set state

        inputActions = new InputActions();
    }

    /////// TURN ON INPUT ///////
    private void OnEnable()
    {
        inputActions.Enable();
    } 

    /////// PER FRAME ACTIONS ///////
    private void Update() 
    {
        switch (state) 
        {
        case State.Normal:
            Vector2 move = inputActions.Player.Move.ReadValue<Vector2>();
            Debug.Log(move);

            bool isIdle = inputActions.Player.Move.ReadValue<Vector2>() == Vector2.zero;

            if (isIdle) 
            {
                //idle animation
            } 

            else 
            {
                //move animation
                
                playerRigidbody2D.MovePosition(transform.position + (Vector3)move * SPEED * Time.fixedDeltaTime);
            }
            break;
        }
    }
}
