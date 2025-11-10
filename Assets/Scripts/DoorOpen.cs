using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private int sceneToLoad, sceneToUnload;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer buttonSpriteRenderer;
    [SerializeField] private InputAction inputAction;
    [SerializeField] private PlayerSO pSO;
    [SerializeField] private Transform player;
    [SerializeField] private bool inHallway;

    private BoxCollider2D doorCollider;
    private bool playerInRange = false;

    private void OnEnable()
    {
        doorCollider = GetComponent<BoxCollider2D>();
        inputAction.Enable();
        inputAction.performed += OpenDoor;
    }

    private void Start()
    {
        buttonSpriteRenderer.enabled = false;
    }

    private void OnDisable()
    {
        inputAction.performed -= OpenDoor;
        inputAction.Disable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            buttonSpriteRenderer.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            buttonSpriteRenderer.enabled = false;
        }
    }

    private void OpenDoor(InputAction.CallbackContext context)
    {
        if (playerInRange)
        {
            StartCoroutine(OpenDoorCoroutine());
        }
    }

    private IEnumerator OpenDoorCoroutine()
    {
        spriteRenderer.enabled = false;
        if (inHallway) pSO.SetXPosition(player);
        // Uncomment if you have a screen fade effect
        // gameManager.StartScreenFade();

        yield return new WaitForSeconds(0.6f);

        GameManager.instance.GetScenesManager().UnloadAndAdd(sceneToUnload, sceneToLoad);
    }
}
