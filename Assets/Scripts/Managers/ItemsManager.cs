using System;
using System.Collections.Generic;
public class ItemsManager
{
    private List<int> items;
    public ItemsManager()
    {
        items = new List<int>();
    }

    public bool IsItemAltered(int x)
    {
        return items.Contains(x);
    }

    public void AddItem(int x)
    {
        items.Add(x);
    }

    public void ResetItems()
    {
        items.Clear();
    }
}
