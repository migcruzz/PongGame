using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float minDirection = 0.5f;
    public float baseXDirection = 0.5f;
    public float baseYDirection = 0f;
    public float baseZDirection = 0.5f;

    [Header("Tags")]
    public string wallTag = "Wall";
    public string racketTag = "Racket";

    private Rigidbody rb;
    private Vector3 direction;
    private bool isStopped = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (GameConfig.Instance != null)
            speed = GameConfig.Instance.GetBallSpeed();

        direction = new Vector3(baseXDirection, baseYDirection, baseZDirection);
        ChooseRandomDirection();

        Debug.Log($"Ball initialized with speed: {speed}");
    }

    private void FixedUpdate()
    {
        if (!isStopped)
        {
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(wallTag))
        {
            direction.z *= -1f;
        }
        else if (other.CompareTag(racketTag))
        {
            Vector3 newDirection = (transform.position - other.transform.position).normalized;

            newDirection.x = Mathf.Sign(newDirection.x) * Mathf.Max(Mathf.Abs(newDirection.x), minDirection);
            newDirection.z = Mathf.Sign(newDirection.z) * Mathf.Max(Mathf.Abs(newDirection.z), minDirection);

            direction = newDirection;
        }
    }

    private void ChooseRandomDirection()
    {
        float signX = Mathf.Sign(Random.Range(-1f, 1f));
        float signZ = Mathf.Sign(Random.Range(-1f, 1f));

        direction = new Vector3(baseXDirection * signX, baseYDirection, baseZDirection * signZ);
    }

    public void Stop()
    {
        isStopped = true;
    }

    public void Go()
    {
        ChooseRandomDirection();
        isStopped = false;
    }
}
