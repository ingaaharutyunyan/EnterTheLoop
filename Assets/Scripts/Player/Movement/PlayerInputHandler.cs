using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputAction walkAction;
    [SerializeField] private PlayerSO playerSO;
    private SpriteRenderer spriteRenderer;

    public float MoveInput { get; private set; }
    public bool MovementAllowed { get; private set; } = true;

    private Tween flipTween;
    private Sequence squeezeSequence;
    private Vector3 originalScale;
    private bool left = false;

    private void OnEnable()
    {
        walkAction.started += OnWalkPerformed;
        walkAction.canceled += OnWalkCanceled;
        playerSO.SetPlayerMovementAllowed += SetPlayerMovement;
        walkAction.Enable();
    }

    private void OnDisable()
    {
        walkAction.Disable();
        walkAction.started -= OnWalkPerformed;
        walkAction.canceled -= OnWalkCanceled;

        if (playerSO != null)
        {
            playerSO.SetPlayerMovementAllowed -= SetPlayerMovement;
        }
        squeezeSequence?.Kill();
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
    }

    public void SetPlayerMovement(bool isAllowed)
    {
        MovementAllowed = isAllowed;

        if (isAllowed)
            walkAction.Enable();
        else
            walkAction.Disable();
    }

    private void OnWalkPerformed(InputAction.CallbackContext ctx)
    {
        MoveInput = ctx.ReadValue<float>();
        string keyName = ctx.control.name;

        if (keyName == "a" || keyName == "leftArrow")
        {
            if (!left) PlaySqueeze();
            left = true;
            spriteRenderer.flipX = true;
        }
        else if (keyName == "d" || keyName == "rightArrow")
        {
            if (left) PlaySqueeze();
            left = false;
            spriteRenderer.flipX = false;
        }
    }

    private void OnWalkCanceled(InputAction.CallbackContext ctx)
    {
        MoveInput = 0f;
    }

    public void PlaySqueeze()
    {
        squeezeSequence?.Kill(); // Kill previous sequence if it's still running

        squeezeSequence = DOTween.Sequence();
        squeezeSequence.Append(transform.DOScaleX(0f, 0.1f).SetEase(Ease.InOutQuad));
        squeezeSequence.Append(transform.DOScaleX(originalScale.x, 0.1f).SetEase(Ease.InOutQuad));
    }
}
