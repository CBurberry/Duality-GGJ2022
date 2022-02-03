using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerRenderer : MonoBehaviour
{
    public string defaultSortingLayer = "Player";
    public string hidingSortingLayer = "PlayerHiding";
    private Color playerHiddenColor = Color.grey; 
    private Color playerColor = Color.white;

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

    [Button]
    public void SetHiding()
    {
        foreach (var renderer in renderers)
        {
            renderer.sortingLayerName = hidingSortingLayer;
        }
    }

    [Button]
    public void SetDefault()
    {
        foreach (var renderer in renderers)
        {
            renderer.sortingLayerName = defaultSortingLayer;
        }
    }

    //Changes player colour and called when hiding.
    public void PlayerColor(bool _aHidden)
    {
        if (_aHidden)
        {
            foreach(SpriteRenderer spriteRenderer in renderers)
            {
                spriteRenderer.color = playerHiddenColor;
            }
        }
        else if (!_aHidden)
        {
            foreach(SpriteRenderer spriteRenderer in renderers)
            {
                spriteRenderer.color = playerColor;
            }
        }
    }
}
