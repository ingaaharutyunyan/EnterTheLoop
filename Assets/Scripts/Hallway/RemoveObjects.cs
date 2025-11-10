using System.Collections.Generic;

public class RemoveObjects
{
    private int counter = -1;

    private Dictionary<int, bool> itemsGUID = new Dictionary<int, bool>
    {
        { 0, false },
        { 1, false },
        { 2, true },
        { 3, false },
        { 5, false },
        { 6, true },
        { 7, false },
        { 8, false },
        { 9, false }
    };

    public (int, bool)? NextIteration()
    {
        if (counter > 9) GameManager.instance.LoseGame();
        counter++;

        if (itemsGUID.ContainsKey(counter))
            return NextGUID();
        else
            return null; // No more items
    }

    private (int, bool) NextGUID()
    {
        return (counter, itemsGUID[counter]);
    }
}
