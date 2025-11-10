using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Safe : MonoBehaviour
{
    [Header("Keypad Setup")]
    [SerializeField] private GameObject keypad;
    [SerializeField] private TMP_Text codeDisplay;
    [SerializeField] private List<SpriteRenderer> digitSprites; // 9 digits: index 0 = 1, index 1 = 2, ..., index 8 = 9

    [Header("Visual Settings")]
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color pressedColor = Color.gray;
    [SerializeField] private PlayerSO pSO;
    [SerializeField] private Notif notif;

    private string currentInput = "";
    private string correctCode = "2963";

    void Start()
    {
        keypad.SetActive(false);
        ResetKeypad();
    }

    void Update()
    {
        if (!keypad.activeSelf) return;

        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                AddDigit(i.ToString());
                HighlightDigit(i - 1); // index 0 = key 1
                break;
            }
        }
    }

    void AddDigit(string digit)
    {
        currentInput += digit;
        codeDisplay.text = currentInput;

        if (currentInput.Length == correctCode.Length)
        {
            if (currentInput == correctCode)
            {
                notif.StartAnimation();
                pSO.EnableScrewdriver();
                // TODO: Trigger reward here
            }
            else
            {
                Debug.Log("Incorrect code. Resetting...");
            }

            ResetKeypad();
        }
    }

    void HighlightDigit(int index)
    {
        if (index >= 0 && index < digitSprites.Count)
        {
            SpriteRenderer sr = digitSprites[index];
            sr.color = pressedColor;
            StartCoroutine(ResetSpriteColorAfterDelay(sr, 0.2f));
        }
    }

    IEnumerator ResetSpriteColorAfterDelay(SpriteRenderer sr, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (sr != null)
            sr.color = defaultColor;
    }

    void ResetKeypad()
    {
        currentInput = "";
        codeDisplay.text = "";
        foreach (SpriteRenderer sr in digitSprites)
        {
            sr.color = defaultColor;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            keypad.SetActive(true);
            ResetKeypad();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            keypad.SetActive(false);
            ResetKeypad();
        }
    }
}

