using UnityEngine;
using Movement; // Importing the namespace for movement states
public class PlayerManager : MonoBehaviour
{
    private State currentState;
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private Rigidbody2D rb;


     void Awake()
    {
        currentState = new IdleState(rb);
    }
    void FixedUpdate()
    {
        // Handle Movement State
        if (currentState != null)
        {
            State newMovementState = currentState.OnUpdate(inputHandler);
            if (newMovementState != null)
            {
                currentState.OnExit();
                currentState = newMovementState;
                currentState.OnEnter();
            }
        }
    }
}