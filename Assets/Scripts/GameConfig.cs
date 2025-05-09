using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public static GameConfig Instance;

    private float ballSpeed = 5f;
    private float racketSpeed = 4f;
    private bool isMultiplayer = false;

    public bool HasBeenConfigured { get; set; } = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("GameConfig initialized and marked to not be destroyed on load");
    }

    public void SetBallSpeed(float value)
    {
        ballSpeed = value;
        Debug.Log("Ball speed set to: " + value);
    }

    public float GetBallSpeed()
    {
        Debug.Log("Ball speed retrieved: " + ballSpeed);
        return ballSpeed;
    }

    public void SetRacketSpeed(float value)
    {
        racketSpeed = value;
        Debug.Log("Racket speed set to: " + value);
    }

    public float GetRacketSpeed()
    {
        Debug.Log("Racket speed retrieved: " + racketSpeed);
        return racketSpeed;
    }

    public void SetIsMultiplayer(bool value)
    {
        isMultiplayer = value;
        Debug.Log("Multiplayer mode set to: " + value);
    }

    public bool GetIsMultiplayer()
    {
        Debug.Log("Multiplayer mode retrieved: " + isMultiplayer);
        return isMultiplayer;
    }
}
