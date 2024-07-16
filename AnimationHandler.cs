using UnityEngine;

public class AnimationHandler : MonoBehaviour
{   
    [Header("The Character Animator")]
    [SerializeField]
    private Animator animator; // Reference to the Animator component

    [Header("Setup")]
    [SerializeField]
    private float smoothing = 0.03f; // Smoothing factor for transitions
    [SerializeField]
    private float runningSmoothing = 0.03f; // Smoothing factor for running transitions

    // Target values for x and y parameters
    private float targetX = 0f;
    private float targetY = 0f;

    // Current values for x and y parameters
    private float currentX = 0f;
    private float currentY = 0f;

    // Track if the shift key was held before movement
    private bool shiftHeldBeforeMovement = false;

    void Update()
    {
        // Check if Shift is pressed
        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Determine which smoothing factor to use
        float currentSmoothing = isRunning ? runningSmoothing : smoothing;

        // Reset target values
        targetX = 0f;
        targetY = 0f;

        // WASD input handling
        if (Input.GetKey(KeyCode.W))
        {
            targetY = isRunning ? 1f : 0.5f; // Forward walk or run
        }
        else if (Input.GetKey(KeyCode.S))
        {
            targetY = isRunning ? -1f : -0.5f; // Backward walk or run
        }

        if (Input.GetKey(KeyCode.A))
        {
            targetX = isRunning ? -1f : -0.5f; // Strafe left walk or run
        }
        else if (Input.GetKey(KeyCode.D))
        {
            targetX = isRunning ? 1f : 0.5f; // Strafe right walk or run
        }

        // Smoothly interpolate current values towards target values
        currentX = Mathf.Lerp(currentX, targetX, currentSmoothing);
        currentY = Mathf.Lerp(currentY, targetY, currentSmoothing);

        // Apply the smoothed x and y parameters to the Animator
        animator.SetFloat("x", currentX);
        animator.SetFloat("y", currentY);

        // Update shiftHeldBeforeMovement
        shiftHeldBeforeMovement = isRunning && Input.GetKey(KeyCode.W);
    }
}
