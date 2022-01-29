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
            activated = true;
            SetObjectsActive(true);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (deactivateOnExit && collision.CompareTag(activatorTag))
        {
            activated = false;
            SetObjectsActive(false);
        }
    }

    private void SetObjectsActive(bool isActive)
    {
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
