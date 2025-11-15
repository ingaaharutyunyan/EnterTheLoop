using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObject/Player")]
public class PlayerSO : ScriptableObject
{
    private List<Vector2> hallwayData;

    private void OnEnable()
    {
        // Initialize if entering play mode or reloading domain
        if (hallwayData == null)
            hallwayData = new List<Vector2>();

        duckActive = false;
        flashlightActive = false;
        screwdriverActive = false;
    }

    public void InstantiateHallwayData()
    {
        hallwayData = new List<Vector2>();
        hallwayData.Add(new Vector2(-17.7f, 0f));
        hallwayData.Add(new Vector2(0f, 0f));
        hallwayData.Add(new Vector2(17.7f, 0f));

    }

    public void ResetHallwayData()
    {
        hallwayData.Clear();
    }

    public void SaveHallway(int index, Vector2 pos)
    {
        if (index < 0) return;

        while (hallwayData.Count <= index)
            hallwayData.Add(Vector2.zero);

        hallwayData[index] = pos;
    }

    public Vector2 LoadHallway(int index)
    {
        if (index < 0 || index >= hallwayData.Count)
            return Vector2.zero;

        return hallwayData[index];
    }

    [SerializeField] private float xPosition;

    public void SetXPosition(Transform player)
    {
        xPosition = player.position.x;
    }

    public void SetXPosition(float x)
    {
        xPosition = x;
    }

    public float GetXPosition()
    {
        return xPosition;
    }

    public void SetMovementAllowed(bool b)
    {
        SetPlayerMovementAllowed?.Invoke(b);
    }

    public void EnableDuck()
    {
        SetDuckActive?.Invoke();
        duckActive = true;
    }

    public void EnableScrewdriver()
    {
        SetScrewdriverActive?.Invoke();
        screwdriverActive = true;
    }

    public void EnableFlashlight()
    {
        SetFlashlightActive?.Invoke();
        flashlightActive = true;
    }

    public bool GetDuckActive() => duckActive;
    public bool GetScrewDriverActive() => screwdriverActive;
    public bool GetFlashlightActive() => flashlightActive;

    public bool HasMeat() => hasMeat;
    public void SetHasMeat(bool b) => hasMeat = b;

    private bool hasMeat;
    private bool duckActive, screwdriverActive, flashlightActive;

    public event Action<bool> SetPlayerMovementAllowed;
    public event Action SetDuckActive, SetScrewdriverActive, SetFlashlightActive;
}
