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
        Debug.Log("✅ GameConfig inicializado (DontDestroyOnLoad)");
    }

    public void SetBallSpeed(float value)
    {
        ballSpeed = value;
        Debug.Log($"🎯 Ball speed definida para: {value}");
    }

    public float GetBallSpeed()
    {
        Debug.Log($"📥 Ball speed lida: {ballSpeed}");
        return ballSpeed;
    }

    public void SetRacketSpeed(float value)
    {
        racketSpeed = value;
        Debug.Log($"🎯 Racket speed definida para: {value}");
    }

    public float GetRacketSpeed()
    {
        Debug.Log($"📥 Racket speed lida: {racketSpeed}");
        return racketSpeed;
    }

    public void SetIsMultiplayer(bool value)
    {
        isMultiplayer = value;
        Debug.Log($"🎯 Modo multiplayer definido como: {value}");
    }

    public bool GetIsMultiplayer()
    {
        Debug.Log($"📥 Modo multiplayer lido: {isMultiplayer}");
        return isMultiplayer;
    }
}
