using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    private float timer;
    [SerializeField] private TextMeshProUGUI textie;
    void Start()
    {
        GameManager.instance.GetMeatEaterManager().TimerChanged += InitTimer;
    }
    void InitTimer(float t)
    {
        timer = t;
        textie.text = Mathf.FloorToInt(t).ToString();

    }
}
