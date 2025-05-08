using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class RacketController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private Key positiveKey = Key.W;
    [SerializeField] private Key negativeKey = Key.S;
    [SerializeField] private bool isPlayer = true;

    [Header("Ball Reference (for AI only)")]
    [SerializeField] private string ballTag = "Ball";

    private Rigidbody rb;
    private Transform ball;
    private InputAction moveAction;
    private float moveInput;

    private void Awake()
    {
        if (isPlayer)
            SetupPlayerInput();
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
        {
            GameObject ballObj = GameObject.FindGameObjectWithTag(ballTag);
            if (ballObj != null)
                ball = ballObj.transform;
            else
                Debug.LogWarning("Ball not found by tag in AI RacketController.");
        }
    }

    private void FixedUpdate()
    {
        if (isPlayer)
        {
            rb.linearVelocity = Vector3.forward * moveInput * speed;
        }
        else
        {
            if (ball == null)
            {
                rb.linearVelocity = Vector3.zero;
                return;
            }

            int direction = ball.position.z.CompareTo(transform.position.z);

            rb.linearVelocity = direction switch
            {
                1 => Vector3.forward * speed,
                -1 => Vector3.back * speed,
                _ => Vector3.zero
            };
        }
    }

    private void SetupPlayerInput()
    {
        string positiveBinding = $"<Keyboard>/{positiveKey.ToString().ToLower()}";
        string negativeBinding = $"<Keyboard>/{negativeKey.ToString().ToLower()}";

        moveAction = new InputAction(name: "Move", type: InputActionType.Value);

        moveAction.AddCompositeBinding("1DAxis")
            .With("Negative", negativeBinding)
            .With("Positive", positiveBinding);

        moveAction.performed += ctx => moveInput = ctx.ReadValue<float>();
        moveAction.canceled += _ => moveInput = 0f;
    }
}
