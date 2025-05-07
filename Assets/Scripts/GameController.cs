using UnityEngine;

public class GameController : MonoBehaviour
{


    private int scoreLeft = 0;
    private int scoreRight = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreGoalLeft(){
        Debug.Log("ScoreGoalLeft");
        this.scoreRight += 1;
    }

    public void ScoreGoalRight(){
        Debug.Log("ScoreGoalRight");
        this.scoreLeft += 1;
    }


}
