using UnityEngine;
using DG.Tweening;

public class MeatEaterHallway : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartMoving();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.LoseGame();
        }
    }
    private void StartMoving()
    {
        float distance = Mathf.Abs(this.transform.position.x - player.transform.position.x);

        this.transform
            .DOMoveX(player.transform.position.x, distance)
            .SetEase(Ease.Linear)
            .OnComplete(StartMoving);

                this.transform
        .DOMoveY(player.transform.position.y + 0.5f, 1f)
        .SetEase(Ease.InOutSine)
        .SetLoops(-1, LoopType.Yoyo);
    }
}