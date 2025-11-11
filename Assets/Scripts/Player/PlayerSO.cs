using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObject/Player")]
public class PlayerSO : ScriptableObject
{
    [SerializeField] private float xPosition;

    private void OnEnable()
    {
        duckActive = false;
        flashlightActive = false;
        screwdriverActive = false;
    }

    public void SetXPosition(Transform player)
    {
        xPosition = player.position.x;
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

    public bool GetDuckActive()
    {
        return duckActive;
    }

    public bool GetScrewDriverActive()
    {
        return screwdriverActive;
    }

    public bool GetFlashlightActive()
    {
        return flashlightActive;
    }

    public bool HasAlmonds()
    {
        return almonds;
    }
    public void SetAlmonds(bool b)
    {
        almonds = b;
    }
    private bool almonds;
    public event Action<bool> SetPlayerMovementAllowed;
    public event Action SetDuckActive, SetScrewdriverActive, SetFlashlightActive;
    private bool duckActive, screwdriverActive, flashlightActive;
}
