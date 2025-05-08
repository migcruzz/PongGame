using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public static GameConfig Instance; // Acesso global

    private float ballSpeed = 5f;
    private bool isMultiplayer = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Getters e Setters
    public float GetBallSpeed() => ballSpeed;
    public void SetBallSpeed(float value) => ballSpeed = value;

    public bool GetIsMultiplayer() => isMultiplayer;
    public void SetIsMultiplayer(bool value) => isMultiplayer = value;
}
