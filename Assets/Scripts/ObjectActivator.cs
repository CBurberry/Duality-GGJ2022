using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    [SerializeField] protected string activatorTag = null;
    [SerializeField] protected bool deactivateOnExit = false;
    [SerializeField] protected GameObject[] objects = null;

    protected bool isInTrigger = false;

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
            isInTrigger = true;
            foreach (var obj in objects)
                obj.SetActive(true);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (deactivateOnExit && collision.CompareTag(activatorTag))
        {
            isInTrigger = false;
            foreach (var obj in objects)
                obj.SetActive(false);
        }
    }
}
