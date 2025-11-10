using System;
using UnityEngine;

public class ItemGUID : MonoBehaviour // ERROR: Link playreprefs to the save slots logic
{
    private const string GUIDPlayerPrefsKey = "ItemGUID";
    [SerializeField] private int guid;

    private void Awake()
    {
        PlayerPrefs.SetString(GUIDPlayerPrefsKey, guid.ToString());
        PlayerPrefs.Save(); // Save PlayerPrefs to disk
    }

    public int GetGUID()
    {
        return guid;
    }
}
