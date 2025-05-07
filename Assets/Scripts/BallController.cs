using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    //Variables not hardcoded for quick Change in case of value changing
    private string wallTagName;
    private string racketTagName;
    private float baseXDirectionValue;
    private float baseYDirectionValue;
    private float baseZDirectionValue;

    public float speed;
    
    public float minDirection;
    private Vector3 direction;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.wallTagName = "Wall";
        this.racketTagName = "Racket";
        this.baseXDirectionValue = 0.5f;
        this.baseYDirectionValue = 0f;
        this.baseZDirectionValue = 0.5f;
        this.minDirection = 0.5f;


        this.rb = GetComponent<Rigidbody>();
        this.direction = new Vector3(baseXDirectionValue,baseYDirectionValue,baseZDirectionValue);
        this.ChooseDirection();

    }

    // Update is called once per frame
    void Update()
    {
        // Method for testing the ball moving
        //transform.position += direction * speed * Time.deltaTime;
    }


     void FixedUpdate()
    {
        this.rb.MovePosition(this.rb.position + direction * speed * Time.fixedDeltaTime);
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(wallTagName)){

            direction.z = direction.z * -1;

        }

        if(other.CompareTag(racketTagName)){

            Vector3 newDirection = (transform.position - other.transform.position).normalized;

            newDirection.x = Mathf.Sign(newDirection.x) *  Mathf.Max(Mathf.Abs(newDirection.x), this.minDirection);
            newDirection.z = Mathf.Sign(newDirection.z) *  Mathf.Max(Mathf.Abs(newDirection.z), this.minDirection);

            direction = newDirection;

        }

    }




  private void ChooseDirection()
{
    float xRangeMin = -1f;
    float yRangeMin = -1f;
    float xRangeMax = 1f;
    float yRangeMax = 1f;

    float signX = Mathf.Sign(UnityEngine.Random.Range(xRangeMin, xRangeMax));
    float signZ = Mathf.Sign(UnityEngine.Random.Range(yRangeMin, yRangeMax));

    this.direction = new Vector3(baseXDirectionValue * signX, baseYDirectionValue, baseZDirectionValue * signZ);
}

    
}
