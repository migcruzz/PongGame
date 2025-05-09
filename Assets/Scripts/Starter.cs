using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Starter : MonoBehaviour
{
    private GameController gameController;
    private Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
        gameController = FindAnyObjectByType<GameController>();
    }

    public void StartCountdown()
    {
        
            animator.SetTrigger("StartCountdown");
        
    }

    public void StartGame()
    {
        
            gameController.StartGame();
        
    }
}
