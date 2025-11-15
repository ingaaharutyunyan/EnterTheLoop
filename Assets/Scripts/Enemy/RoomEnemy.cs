using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
public class RoomEnemy : MonoBehaviour
{
    [SerializeField] private Light2D[] spotLight;
    [SerializeField] private GameObject buttonPrompt, countDown, enemyObject, vignette;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] countDownSprites;
    private bool failed = true;

    void Awake()
    {
        int chance = Random.Range(0, 100);
        if (chance < 30)
        {
            foreach (Light2D s in spotLight) s.intensity = 0.01f;
            buttonPrompt.SetActive(true);
            enemyObject.SetActive(true);
            StartCoroutine(ContinueEnemy());
        }
        else this.gameObject.SetActive(false);
    }

    public void ActivateEnemy()
    {
        enemyObject.SetActive(true);
    }

    private IEnumerator ContinueEnemy()
    {
        Count(0);
        ShakeVignette();    
        yield return new WaitForSeconds(3f);
        if (failed) GameManager.instance.LoseGame();
        else yield return null;
    }
    
    private void ShakeVignette()
    {
        if (!failed) return;
        vignette.transform.DOShakePosition(0.5f, new Vector2(0.5f, 0.5f), 10, 90, false, true).OnComplete(ShakeVignette);
    }

    public void Count(int i)
    {
        if (i > 2 || !failed) return;
        countDown.transform.DOScale(new Vector2(0.8f, 0.8f), 0.5f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => {
                spriteRenderer.sprite = countDownSprites[i];
                countDown.transform.DOScale(new Vector2(0f, 0f), 0.5f)
                    .SetEase(Ease.InQuad)
                    .OnComplete(() => { Count(i + 1); });
            });
    }

    public void DefeatEnemy()
    {
        foreach (Light2D s in spotLight) s.intensity = 1f;
        failed = false;
        this.gameObject.SetActive(false);
    }
}
