using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerRenderer : MonoBehaviour
{
    public string defaultSortingLayer = "Player";
    public string hidingSortingLayer = "PlayerHiding";

    SpriteRenderer[] renderers;

    // Start is called before the first frame update
    void Awake()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
        if (renderers == null)
        {
            Debug.Log("No SpriteRenderers were found on Player!");
            return;
        }

        foreach (var renderer in renderers)
        {
            renderer.sortingLayerName = defaultSortingLayer;
        }
    }

    public void SetHiding()
    {
        foreach (var renderer in renderers)
        {
            renderer.sortingLayerName = defaultSortingLayer;
        }
    }

    public void SetDefault()
    {
        foreach (var renderer in renderers)
        {
            renderer.sortingLayerName = defaultSortingLayer;
        }
    }
}
