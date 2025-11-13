using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class RoomEnemy : MonoBehaviour
{
    [SerializeField] private Light2D[] spotLight;
    [SerializeField] private GameObject buttonPrompt;
    [SerializeField] private GameObject enemyObject;
    private bool failed = true;

    void Awake()
    {
        int chance = Random.Range(0, 100);
        if (chance < 50)
        {
            foreach (Light2D s in spotLight) s.intensity = 0.01f;
            buttonPrompt.SetActive(true);
            enemyObject.SetActive(true);
            StartCoroutine(ContinueEnemy());
        }
    }

    public void ActivateEnemy()
    {
        enemyObject.SetActive(true);
    }

    private IEnumerator ContinueEnemy()
    {
        yield return new WaitForSeconds(3f);
        if (failed) GameManager.instance.LoseGame();
        else yield return null;
    }

    public void DefeatEnemy()
    {
        failed = false;
        enemyObject.SetActive(false);
        buttonPrompt.SetActive(false);
    }
}
