using UnityEngine;
using TMPro;
using System.Collections;

public class HintSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textie;
    [SerializeField] private GameObject textbubble;
    [SerializeField] private PlayerSO pSO;

    private string[] hints = {
        "Man, if only there was a safe with some important thing in it",
        "If only somebody drew the code to the safe on the walls somewhere",
        "Maaan, if only there was a flashlight that also had a blue light option to see the writings on the walls",
        "MAN, if only you would hurry up!!"
    };

    private int counter = 0;

    public void OnEnable()
    {
        textbubble.SetActive(false);
        StartCoroutine(Bruh());
    }

    private IEnumerator Bruh()
    {
        while (counter < hints.Length && pSO.GetScrewDriverActive())
        {
            yield return new WaitForSeconds(60f);
            textie.text = hints[counter];
            textbubble.SetActive(true);

            yield return new WaitForSeconds(10f);
            textbubble.SetActive(false);

            counter++;
        }
    }
}
