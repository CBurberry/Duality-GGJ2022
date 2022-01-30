using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// Base gameplay trigger for any overlapping behaviour
/// </summary>
public abstract class GameplayTrigger : MonoBehaviour
{
    public bool isRepeatable = false;

    [Tag]
    public string activatorTag = "Player"; 

    protected virtual void Awake()
    {
        var colliderComponent = GetComponent<Collider2D>();
        if (colliderComponent == null)
        {
            Debug.LogError($"{name}'s '{GetType().Name}' component requires a '{nameof(Collider2D)}' component on the same GameObject!");
        }
        else
        {
            if (!colliderComponent.isTrigger)
            {
                Debug.LogError($"{name}'s '{GetType().Name}' component requires the '{nameof(Collider2D)}' component be set as a trigger collider!");
            }
        }
    }

    //Override in derived classes.
    protected abstract void ExecuteTriggerBehaviour();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(activatorTag))
        {
            ExecuteTriggerBehaviour();
            if (!isRepeatable)
            {
                Destroy(gameObject);
            }
        }
    }
}
