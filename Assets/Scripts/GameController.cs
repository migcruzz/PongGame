using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(GameController))]
public class GameController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Text scoreTextLeft;
    [SerializeField] private Text scoreTextRight;

    [Header("Gameplay Settings")]
    [SerializeField] private GameObject ball;
    [SerializeField] private Key startKey = Key.Space;

    [Header("References")]
    [SerializeField] private Starter starter;
    [SerializeField] private PauseController pauseController;

    private BallController ballController;
    private InputAction startGameAction;
    private InputAction pauseAction;
    private Vector3 startingPosition;

    private bool started = false;
    private int scoreLeft = 0;
    private int scoreRight = 0;

    private void Awake()
    {
        SetupStartGameInput();
        SetupPauseInput();
    }

    private void Start()
    {
        if (ball == null)
        {
            Debug.LogError("Ball GameObject is not assigned.");
            enabled = false;
            return;
        }

        ballController = ball.GetComponent<BallController>();
        if (ballController == null)
        {
            Debug.LogError("BallController script not found on Ball GameObject.");
            enabled = false;
            return;
        }

        startingPosition = ball.transform.position;

        startGameAction.performed += ctx =>
        {
            if (!started)
            {
                started = true;
                starter.StartCountdown();
            }
        };

        startGameAction.Enable();
    }

    private void OnDestroy()
    {
        startGameAction?.Disable();
        startGameAction?.Dispose();

        pauseAction?.Disable();
        pauseAction?.Dispose();
    }

    private void SetupStartGameInput()
    {
        string binding = $"<Keyboard>/{startKey.ToString().ToLower()}";
        startGameAction = new InputAction(name: "StartGame", type: InputActionType.Button, binding: binding);
    }

    private void SetupPauseInput()
    {
        pauseAction = new InputAction(name: "Pause", type: InputActionType.Button, binding: "<Keyboard>/escape");
        pauseAction.performed += ctx => pauseController.TogglePause();
        pauseAction.Enable();
    }

    public void StartGame()
    {
        ballController.Go();
    }

    public void ScoreGoalLeft()
    {
        scoreRight++;
        Debug.Log("Goal scored on left side");
        UpdateUI();
        ResetGame();
    }

    public void ScoreGoalRight()
    {
        scoreLeft++;
        Debug.Log("Goal scored on right side");
        UpdateUI();
        ResetGame();
    }

    private void UpdateUI()
    {
        if (scoreTextLeft != null) scoreTextLeft.text = scoreLeft.ToString();
        if (scoreTextRight != null) scoreTextRight.text = scoreRight.ToString();
    }

    private void ResetGame()
    {
        started = false;
        ballController.Stop();
        ball.transform.position = startingPosition;
        starter.StartCountdown();
    }
}
