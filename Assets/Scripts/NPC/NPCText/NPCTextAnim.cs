using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCTextAnim : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textie;
    [SerializeField] private string[] lines;
    private int index;
    private bool finished = true;
    public float speed {get; set;}
    public bool GetFinished() {return finished;}
    public int GetRemainingLinesLength()
    {
        return lines.Length - index;
    }
    public void StartDialogue()
    {
        index = 0;
        textie.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        finished = false;
        foreach (char c in lines[index].ToCharArray())
        {
            textie.text += c;
            yield return new WaitForSeconds(speed);
        }
        finished = true;
    }

    public void NextLine()
    {
        if (index < lines.Length)
        {
                speed = 0.005f;
                textie.text = string.Empty;
                StartCoroutine(TypeLine());
                index++; 
        }
    }

    public void SkipLines(int count)
    {
        index += count;
    }

    public void SpeedLine()
    {
        speed = 0.000001f;
    }

    public void ResetIndex()
    {
        index = 0;
    }
}
