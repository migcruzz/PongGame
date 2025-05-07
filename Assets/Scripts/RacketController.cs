using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class RacketController : MonoBehaviour
{
    public float speed = 5f;
    public Key positiveKey = Key.W;
    public Key negativeKey = Key.S;

    private Rigidbody rb;
    private InputAction moveAction;
    private float moveInput;

    private void Awake()
    {
        string positiveBinding = $"<Keyboard>/{positiveKey.ToString().ToLower()}";
        string negativeBinding = $"<Keyboard>/{negativeKey.ToString().ToLower()}";

        moveAction = new InputAction(
            name: "Move",
            type: InputActionType.Value,
            binding: ""
        );

        moveAction.AddCompositeBinding("1DAxis")
            .With("Negative", negativeBinding)
            .With("Positive", positiveBinding);

        moveAction.performed += ctx => moveInput = ctx.ReadValue<float>();
        moveAction.canceled += ctx => moveInput = 0f;
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = Vector3.forward * moveInput * speed;
    }
}
