using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelAsset levelAsset;

    public static LevelManager Ins;

    private void Awake()
    {
        if (!Ins) Ins = this;
    }
}
