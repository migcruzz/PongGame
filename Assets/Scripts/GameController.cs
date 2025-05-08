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

    private BallController ballController;
    private InputAction startGameAction;
    private Vector3 startingPosition;

    private bool started = false;
    private int scoreLeft = 0;
    private int scoreRight = 0;

    private void Awake()
    {
        SetupInputAction();
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

        // Only assign the callback now (after everything is ready)
        startGameAction.performed += ctx =>
        {
            if (!started)
            {
                started = true;
                ballController.Go();
            }
        };

        startGameAction.Enable();
    }

    private void OnDestroy()
    {
        startGameAction?.Disable();
        startGameAction?.Dispose();
    }

    private void SetupInputAction()
    {

        if(started){
            return;
        }

        string binding = $"<Keyboard>/{startKey.ToString().ToLower()}";

        startGameAction = new InputAction(
            name: "StartGame",
            type: InputActionType.Button,
            binding: binding
        );
    }

    public void StartGame()
    {
        ballController.Go();
    }

    public void ScoreGoalLeft()
    {
        scoreRight++;
        Debug.Log("ScoreGoalLeft");
        UpdateUI();
        ResetGame();
    }

    public void ScoreGoalRight()
    {
        scoreLeft++;
        Debug.Log("ScoreGoalRight");
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
        ballController.Go();
    }
}