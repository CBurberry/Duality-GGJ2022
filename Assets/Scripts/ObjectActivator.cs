using NaughtyAttributes;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    [BoxGroup("ObjectActivator")]
    [Tag]
    [SerializeField]
    protected string activatorTag = null;

    [BoxGroup("ObjectActivator")]
    [SerializeField]
    protected bool deactivateOnExit = false;

    [BoxGroup("ObjectActivator")]
    [SerializeField]
    protected GameObject[] activatableObjects = null;

    [BoxGroup("ObjectActivator")]
    [ShowNonSerializedField]
    protected bool activated = false;

    [BoxGroup("ObjectActivator")]
    [ShowNonSerializedField]
    protected bool isOverlappingActivator;

    [BoxGroup("ObjectActivator")]
    [SerializeField]
    private bool activateOnOverlap = false;

    protected virtual void Start()
    {
        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogError($"No Collider2D component is attached to '{GetType().Name}' component on {name}!");
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(activatorTag))
        {
            isOverlappingActivator = true;
            if (activateOnOverlap)
            {
                SetObjectsActive(true);
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (deactivateOnExit && collision.CompareTag(activatorTag))
        {
            isOverlappingActivator = false;
            SetObjectsActive(false);
        }
    }

    protected void SetObjectsActive(bool isActive)
    {
        activated = isActive;
        foreach (var obj in activatableObjects)
            obj.SetActive(isActive);
    }

    [Button("ToggleActive")]
    private void DEBUG_ToggleActive()
    {
        activated = !activated;
        SetObjectsActive(activated);
    }
}
