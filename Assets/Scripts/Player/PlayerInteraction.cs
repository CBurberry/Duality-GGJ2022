using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private Collider2D playerCollider;
    private List<GameObject> overlappingInteractables;
    private Interactable focusedInteractable;
    private bool playerHidden = false;
    private PlayerRenderer playerRenderer;

    [Tag]
    private string interactTag = "Interactable";

    void Awake()
    {
        overlappingInteractables = new List<GameObject>();
        playerCollider = GetComponent<Collider2D>();
        if (playerCollider == null)
        {
            Debug.LogError($"Player '{gameObject.name}' does not have a Collider2D component!");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(interactTag))
        {
            if (!overlappingInteractables.Any(x => ReferenceEquals(x, collision.gameObject)))
            {
                overlappingInteractables.Add(collision.gameObject);
                UpdateFocus();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(interactTag))
        {
            overlappingInteractables.Remove(collision.gameObject);
            UpdateFocus();
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //Debug.Log("Action was started");
            focusedInteractable.Interact();
        }
        else if (context.performed)
        {
            //Do Nothing
            //Debug.Log("Action was performed");
        }
        else if (context.canceled)
        {
            //Do Nothing
            //Debug.Log("Action was cancelled");
        }
    }

    private void UpdateFocus()
    {
        //Get the closest distance gameobject
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in overlappingInteractables.Select(x => x.transform))
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        GameObject newFocus = tMin?.gameObject;
        if (newFocus == null)
        {
            focusedInteractable.SetFocused(false);
            focusedInteractable = null;
            return;
        }

        if (focusedInteractable != null)
        {
            if (ReferenceEquals(newFocus, focusedInteractable.gameObject))
            {
                return;
            }

            //Unfocus the previous element
            focusedInteractable.SetFocused(false);
        }

        //Focus the new element
        focusedInteractable = newFocus.GetComponent<Interactable>();
        focusedInteractable.SetFocused(true);
    }

    public void HideUnHideCharacter(GameObject _hidingPlaceObject)
    {
        playerRenderer = GetComponent<PlayerRenderer>();

        if (playerHidden) //if the player is already hiding, stop hiding.
        {
            Debug.Log("Unhide Character");
            playerHidden = false;
            playerRenderer.PlayerColor(playerHidden); //Restore the player sprite renderer colour.
            playerRenderer.SetDefault(); //Change Sorting Layer;
            GetComponent<PlayerMovement>().SetMovementActive(true); //Start the player moving
            
            //Restarts the monster and player collision from taking place.
            GetComponent<CapsuleCollider2D>().isTrigger = false; 
            
        }
        else if(!playerHidden) //if the player is not hiding, start hiding.
        {
            Debug.Log("Hiding Character");
            playerHidden = true;
            playerRenderer.PlayerColor(playerHidden); //Change the player sprite renderer colour.
            playerRenderer.SetHiding();//Change Sorting Layer;
            GetComponent<PlayerMovement>().SetMovementActive(false); //Stop the player moving
            
            /* Stops the monster and player collision from taking place, 
             * while maintaining trigger collision with hide behind objects.
             * 
             * To be amended with a more elegant solution.
             */
            GetComponent<CapsuleCollider2D>().isTrigger = true;

        }
    }
}
