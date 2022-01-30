using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// Simple gameplay trigger that spawns a prefab at the trigger location & rotation
/// </summary>
public class SpawnTrigger : GameplayTrigger
{
    public enum SpawnType
    {
        LocationOffsetWithRotation,
        UsingTransform
    }

    [BoxGroup("SpawnTrigger")]
    [SerializeField]
    private GameObject prefab;

    [BoxGroup("SpawnTrigger")]
    [SerializeField]
    private SpawnType spawnType;

    [ShowIf("spawnType", SpawnType.LocationOffsetWithRotation)]
    [BoxGroup("SpawnTrigger")]
    [SerializeField]
    private Vector3 spawnOffset;

    [ShowIf("spawnType", SpawnType.LocationOffsetWithRotation)]
    [BoxGroup("SpawnTrigger")]
    [SerializeField]
    private Vector3 rotationEulerAngles;

    [ShowIf("spawnType", SpawnType.UsingTransform)]
    [BoxGroup("SpawnTrigger")]
    [SerializeField]
    private Transform spawnAtTransform;

    [BoxGroup("SpawnTrigger")]
    [SerializeField]
    private GameObjectEvent OnSpawn;

    protected override void Awake()
    {
        base.Awake();

        if (prefab == null)
        {
            Debug.LogError($"{name}'s {nameof(SpawnTrigger)} component does not have a prefab set to spawn!");
        }

        if (spawnType == SpawnType.UsingTransform && spawnAtTransform == null)
        {
            Debug.LogError($"{name}'s {nameof(SpawnTrigger)} component is missing the Transform to spawn at!");
        }
    }

    protected override void ExecuteTriggerBehaviour()
    {
        GameObject instance = null;

        if (spawnType == SpawnType.LocationOffsetWithRotation)
        {
            instance = Instantiate(prefab, transform.position + spawnOffset, Quaternion.Euler(rotationEulerAngles));
        }
        else
        {
            instance = Instantiate(prefab, spawnAtTransform.position, spawnAtTransform.rotation);
        }

        //If an assigned callback is set, pass them a reference to the newly instantiated clone.
        OnSpawn.Invoke(instance);
    }
}
