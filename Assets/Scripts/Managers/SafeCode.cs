using System;
using UnityEngine;

public class SafeCode
{
    private int[] code;
    public SafeCode()
    {
        code = new int[4];
        for (int i = 0; i < 4; i++)
        {
            code[i] = UnityEngine.Random.Range(0, 10);
        }
    }

    public int[] GetCode()
    {
        return code;
    }
}
