using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton class for in-between scenes data persistence and scene transition handler.
/// Add and set flags for individual elements in the scene.
/// </summary>
public class GameManager : MonoBehaviour
{
    //Singleton
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    #region AreaFlags
    public enum AreaOneFlags
    {
        Default = 0,
        //NewFlag = 1 << 0,
        //NewFlag = 1 << 1,
        //NewFlag = 1 << 2
    }

    public enum AreaTwoFlags
    {
        Default = 0,
    }

    public enum AreaThreeFlags
    {
        Default = 0,
    }

    [EnumFlags]
    public AreaOneFlags areaOneFlags;

    [EnumFlags]
    public AreaTwoFlags areaTwoFlags;

    [EnumFlags]
    public AreaThreeFlags areaThreeFlags;
    #endregion

    void Awake()
    {
        //Initialize singleton
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
