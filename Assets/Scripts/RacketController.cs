using UnityEngine;
using UnityEngine.InputSystem;

public class RacketController : MonoBehaviour
{
    private string ballTagName = "Ball";

    public float speed = 5f;
    public Key positiveKey;
    public Key negativeKey;

    public bool isPlayer = true;

    private Rigidbody rb;
    private Transform ball;

    private InputAction moveAction;
    private float moveInput;

    private void Awake()
    {
        if (isPlayer)
        {
            SetupPlayerInput();
        }
    }

    private void SetupPlayerInput()
    {
        string positiveBinding = $"<Keyboard>/{positiveKey.ToString().ToLower()}";
        string negativeBinding = $"<Keyboard>/{negativeKey.ToString().ToLower()}";

        moveAction = new InputAction(
            name: "Move",
            type: InputActionType.Value
        );

        moveAction.AddCompositeBinding("1DAxis")
            .With("Negative", negativeBinding)
            .With("Positive", positiveBinding);

        moveAction.performed += ctx => moveInput = ctx.ReadValue<float>();
        moveAction.canceled += ctx => moveInput = 0f;
    }

    private void OnEnable()
    {
        if (isPlayer)
            moveAction?.Enable();
    }

    private void OnDisable()
    {
        if (isPlayer)
            moveAction?.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (!isPlayer)
            ball = GameObject.FindGameObjectWithTag(ballTagName).transform;
    }

    private void FixedUpdate()
    {
        if (isPlayer)
        {
            rb.linearVelocity = Vector3.forward * moveInput * speed;
        }
        else
        {
            if (ball == null) return;

            int direction = ball.position.z.CompareTo(transform.position.z);

            switch (direction)
            {
                case 1:
                    rb.linearVelocity = Vector3.forward * speed;
                    break;
                case -1:
                    rb.linearVelocity = Vector3.back * speed;
                    break;
                case 0:
                    rb.linearVelocity = Vector3.zero;
                    break;
            }
        }
    }
}
