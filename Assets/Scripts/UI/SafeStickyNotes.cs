using UnityEngine;
using TMPro;

public class SafeStickyNotes : MonoBehaviour
{
    [SerializeField] private GameObject[] noteTexts;
    [SerializeField] private TextMeshProUGUI[] noteTextsTMP;
    private int[] safeCode;

    public void SetupCode(int[] code)
    {
        safeCode = code;
        for (int i = 0; i < noteTexts.Length; i++)
        {
            noteTextsTMP[i].text = safeCode[i].ToString();
        }

        foreach (var text in noteTexts)
        {
            text.gameObject.SetActive(false);
        }
    }

    public void ShowNotes(int x)
    {
        noteTexts[x].gameObject.SetActive(true);
    }

    public void HideAllNotes()
    {
        foreach (GameObject n in noteTexts) n.SetActive(false);
    }
}
