using UnityEngine;
using TMPro;
using System.Collections;
using System.Runtime.ExceptionServices;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] screens;
    public void ShowScreen(int x)
    {
        for (int i = 0; i < screens.Length; i++)
        {
            if (x == i) screens[i].SetActive(true);
            else screens[i].SetActive(false);
        }
    }

    public void OnInstructionClicked()
    {
        ShowScreen(3);
    }

    public void OnBackClicked()
    {
        ShowScreen(0);
    }

}
