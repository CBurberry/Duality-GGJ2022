using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Utility script to facilitate objects having a UI element positioned relative to 
/// a 2D gameobject (in screen space).
/// 
/// N.B. Canvas should be Screen Space - Overlay
/// </summary>
public class AttachedUIPrompt : MonoBehaviour
{
    private Canvas canvas;

    [SerializeField]
    private bool alwaysUpdate = true;

    [SerializeField]
    private Vector2 offset;

    [Label("UI Prompt")]
    public GameObject UIPrompt;

    void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError($"No 'Canvas' GameObject was found in the current Scene! Cannot display AttachedUIPrompt!");
            return;
        }

        UIPrompt.transform.SetParent(canvas.transform);
    }

    void Start()
    {
        if (Camera.main == null)
        {
            Debug.LogError($"'Camera.main' was invalid or unset! Cannot display AttachedUIPrompt!");
            return;
        }

        UpdatePosition();
    }

    void Update()
    {
        if (alwaysUpdate)
        {
            UpdatePosition();
        }
    }

    [Button]
    private void UpdatePosition()
    {
        //Compute current world position in screen space
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        screenPos.x += offset.x;
        screenPos.y += offset.y;

        //Apply new position to UI element
        UIPrompt.transform.position = screenPos;
    }
}
