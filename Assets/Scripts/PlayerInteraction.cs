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
}
