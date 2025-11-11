using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpFlashlight : MonoBehaviour
{
    private BoxCollider2D coll;
    private bool playerInRange = false;
    [SerializeField] private InputAction inputAction;
    [SerializeField] private SpriteRenderer buttonSpriteRenderer;
    [SerializeField] private PlayerSO pSO;
    private ItemsManager itemManager;
    private ItemGUID itemGUID;

    private void OnEnable()
    {
        itemGUID = GetComponent<ItemGUID>();
        itemManager = GameManager.instance.GetItemsManager();
        coll = GetComponent<BoxCollider2D>();
        
        inputAction.performed += GetFlashlight;
    }

    private void Start()
    {
        buttonSpriteRenderer.enabled = false;
    }

    private void OnDisable()
    {
        inputAction.performed -= GetFlashlight;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inputAction.Enable();
            buttonSpriteRenderer.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inputAction.Disable();
            buttonSpriteRenderer.enabled = false;
        }
    }

    private void GetFlashlight(InputAction.CallbackContext context)
    {
        itemManager.AddItem(itemGUID.GetGUID());
        pSO.EnableFlashlight();
        this.gameObject.SetActive(false);
    }
}
