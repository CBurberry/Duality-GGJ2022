using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [SerializeField] private const float SPEED = 4f;

    private GameObject playerBase;
    [SerializeField] private State state;
    private Rigidbody2D playerRigidbody2D;
    private InputActions inputActions;

    [SerializeField] private Animator anim;
    [SerializeField] private bool canTurn = false;

    /////// STATE OPTIONS ///////
    private enum State 
    {
        Normal,
    }
    
    /////// STATE INITIALIZATION ///////
    private void Awake() 
    {
        if (anim == null)
        {
            anim = this.GetComponentInChildren<Animator>();
        }

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
            ////GET THE INPUT AS A VECTOR/////
            Vector2 move = inputActions.Player.Move.ReadValue<Vector2>();
            //Debug.Log(move);

            bool isIdle = inputActions.Player.Move.ReadValue<Vector2>() == Vector2.zero;

            ////TELL THE ANIMATOR ABOUT THE INPUT/////
            float spd = Mathf.Abs(move.x) + Mathf.Abs(move.y);
            anim.SetFloat("RunningSpeed", spd);      
            Debug.Log(canTurn);

            ////MOVE THE PLAYER/////
            playerRigidbody2D.MovePosition(transform.position + (Vector3)move * SPEED * Time.fixedDeltaTime);
            
            //if facing right is true and move.x < 0
            if( canTurn == true && move.x > 0 )
            {
                canTurn = false;
                this.transform.Rotate(new Vector3(0,180,0));
                Debug.Log("turned");
            }
            if( canTurn == false && move.x < 0 )
            {
                canTurn = true;
                this.transform.Rotate(new Vector3(0,180,0));
                Debug.Log("turned");
            }


            break;
        }
    }
}
