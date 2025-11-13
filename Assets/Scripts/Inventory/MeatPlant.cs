using UnityEngine;
using UnityEngine.InputSystem;

public class MeatPlant : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrompt;
    [SerializeField] private InputAction inputAction;
    [SerializeField] private PlayerSO pSO;

    void Start()
    {
        buttonPrompt.SetActive(false);
    }

    void OnEnable()
    {
        inputAction.performed += TakeFromMeatPlant;
    }

    void OnDisable()
    {
        inputAction.performed -= TakeFromMeatPlant;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !pSO.HasMeat())
        {
            buttonPrompt.SetActive(true);
            inputAction.Enable();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buttonPrompt.SetActive(false);
            inputAction.Disable();
        }
    }

    private void TakeFromMeatPlant(InputAction.CallbackContext context)
    {
        pSO.SetHasMeat(true);
        buttonPrompt.SetActive(false);
        inputAction.Disable();
    }
}
