using System;
using UnityEngine;
using System.Collections.Generic;

public class GameAssetsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gameAssets;
    private RemoveObjects rm;

    private void Awake()
    {
        rm = new RemoveObjects();
    }

    public void ChangeNextIteration()
    {
        var result = rm.NextIteration();
        Debug.Log(result);
        if (result == null) return;

        var (index, isRemoved) = result.Value;

        if (index >= 0 && index < gameAssets.Length)
        {
            gameAssets[index].SetActive(isRemoved);
        }
        else
        {
            Debug.LogWarning($"Index {index} is out of bounds for gameAssets array.");
        }
    }

    public bool IsItemAltered(GameObject item)
    {
        var itemGuid = item.GetComponent<ItemGUID>();
        if (itemGuid == null) return false;

        int guid = itemGuid.GetGUID();
        return guid >= 0 && guid < gameAssets.Length && !gameAssets[guid].activeSelf;
    }

    public bool IsItemAltered(int guid)
    {
        return guid >= 0 && guid < gameAssets.Length && !gameAssets[guid].activeSelf;
    }

    public GameObject[] GetGameAssets()
    {
        return gameAssets;
    }

    public void ResetItemStates()
    {
        foreach (var obj in gameAssets)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }
}
