using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public Text scoreTextLeft;
    public Text scoreTextRight;

    public GameObject ball;
    public Key startKey;

    private BallController ballController;
    private InputAction startGameAction;

    private bool started = false;
    private int scoreLeft = 0;
    private int scoreRight = 0;

    void Awake()
    {
        SetupStartGameAction();
    }

    void Start()
    {
        this.ballController = this.ball.GetComponent<BallController>();
        startGameAction.Enable(); // habilita a ação
    }

    void OnDestroy()
    {
        startGameAction?.Disable();
        startGameAction?.Dispose();
    }

    private void SetupStartGameAction()
    {
        string binding = $"<Keyboard>/{startKey.ToString().ToLower()}";

        startGameAction = new InputAction(
            name: "StartGame",
            type: InputActionType.Button,
            binding: binding
        );

        startGameAction.performed += ctx =>
        {
            if (!started)
            {
                started = true;
                ballController.Go();
            }
        };
    }

    public void ScoreGoalLeft()
    {
        Debug.Log("ScoreGoalLeft");
        this.scoreRight += 1;
        UpdateUI();
    }

    public void ScoreGoalRight()
    {
        Debug.Log("ScoreGoalRight");
        this.scoreLeft += 1;
        UpdateUI();
    }

    private void UpdateUI()
    {
        this.scoreTextLeft.text = this.scoreLeft.ToString();
        this.scoreTextRight.text = this.scoreRight.ToString();
    }
}
