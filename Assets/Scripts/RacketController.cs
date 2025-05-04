using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketController : MonoBehaviour
{
    public float speed;
    public KeyCode up;
    public KeyCode down;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        bool pressedUp = Input.GetKey(this.up);
        bool pressedDown = Input.GetKey(this.down);

        if (pressedUp)
        {
            rb.linearVelocity = Vector3.forward * speed;
        }
        else if (pressedDown)
        {
            rb.linearVelocity = Vector3.back * speed;
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }
}
