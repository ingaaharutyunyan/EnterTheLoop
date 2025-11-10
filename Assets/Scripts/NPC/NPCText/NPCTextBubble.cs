using DG.Tweening;
using UnityEngine;

public class NPCTextBubble : MonoBehaviour // controls the animation aspect of the text bubble
{
    void OnEnable()
    {
        TweenThingDown(textBox);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        textBox.SetActive(true);
        if (other.tag == "Player") TweenThingUp(textBox);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        TweenThingDown(textBox);
    }
    public void TweenThingUp(GameObject obj)
    {
        obj.transform.DOScale(1f, 0.3f);
        obj.transform.DORotate(Vector3.zero, 0.3f);
    }

    public void TweenThingDown(GameObject obj)
    {
        obj.transform.DOScale(0f, 0.3f);
        obj.transform.DORotate(new Vector3(0f, 0f, 90f), 0.3f);
    }
    [SerializeField] private GameObject textBox;
}
