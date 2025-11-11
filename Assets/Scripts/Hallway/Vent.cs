using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Vent : MonoBehaviour
{
    [SerializeField] private PlayerSO pSO;
    [SerializeField] private SpriteRenderer labelSpriteRenderer, spriteRenderer;
    [SerializeField] private InputAction inputAction;
    [SerializeField] private Sprite openedVent;
    
    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        inputAction.performed += WinGame;
        labelSpriteRenderer.enabled = false;
    }

    void OnDisable()
    {
        inputAction.performed -= WinGame;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && pSO.GetScrewDriverActive())
        {
            inputAction.Enable();
            labelSpriteRenderer.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inputAction.Disable();
            labelSpriteRenderer.enabled = false;
        }
    }

    private void WinGame(InputAction.CallbackContext ctx)
    {
        StartCoroutine(WinGame());
        
    }

    IEnumerator WinGame()
    {
        spriteRenderer.sprite = openedVent;
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.WinGame();
    }
}
