using System;
using UnityEngine;

public class ItemGUID : MonoBehaviour // ERROR: Link playreprefs to the save slots logic
{
    private const string GUIDPlayerPrefsKey = "ItemGUID";
    [SerializeField] private int guid;
    private ItemsManager itemManager;

    private void Awake()
    {
        PlayerPrefs.SetString(GUIDPlayerPrefsKey, guid.ToString());
        PlayerPrefs.Save(); // Save PlayerPrefs to disk
        itemManager = GameManager.instance.GetItemsManager();
    }
    void OnEnable()
    {
        if (itemManager.IsItemAltered(guid)) this.gameObject.SetActive(false);
    }

    public int GetGUID()
    {
        return guid;
    }
}
