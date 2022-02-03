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
    [SerializeField] CharacterAudio audioPlayer = null;

    /////// STATE OPTIONS ///////
    private enum State 
    {
        Normal,
    }
    
    /////// INITIALIZATION ///////
    private void Awake() 
    {
        /// set the animator
        if (anim == null)
        {
            anim = this.GetComponentInChildren<Animator>();
        }
        /// make this a single instance
        instance = this;
        /// get player components
        playerBase = gameObject.GetComponent<GameObject>();
        playerRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        /// set state
        state = State.Normal;   
        ///get input
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
            
            ////Play Steps/////
            if (move != Vector2.zero)
                audioPlayer.PlaySteps();

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

    public void SetMovementActive(bool _aActive)
    {
        if (_aActive)
        {
            inputActions.Player.Move.Enable();
        }
        else if (!_aActive)
        {
            inputActions.Player.Move.Disable();
        }
    }
}
