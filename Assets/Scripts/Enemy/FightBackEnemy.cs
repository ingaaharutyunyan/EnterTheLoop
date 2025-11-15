using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class FightBackEnemy : MonoBehaviour
{
    [SerializeField] private Transform buttonPrompt;
    [SerializeField] private InputAction inputAction;
    [SerializeField] private RoomEnemy roomEnemy;

    private int clickedButton;
    private Tween bounceTween; // Reference to the active tween

    void OnEnable()
    {
        inputAction.Enable();
        inputAction.performed += FightBack;
    }

    void OnDisable()
    {
        inputAction.performed -= FightBack;
        inputAction.Disable();

        // Kill any active tween to prevent leaks
        bounceTween?.Kill();
    }

    private void FightBack(InputAction.CallbackContext context)
    {
        if (clickedButton >= 5)
        {   roomEnemy.DefeatEnemy();
            return;
        }

        clickedButton++;

        // Kill any currently running tween to restart the bounce
        bounceTween?.Kill();

        // Perform a small "bounce" animation
        float bounceScale = 1.2f;
        float bounceDuration = 0.2f;

        // Start a new bounce tween
        bounceTween = buttonPrompt.DOScale(bounceScale, bounceDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                buttonPrompt.DOScale(1f, bounceDuration).SetEase(Ease.OutBounce);
            });
    }
}
