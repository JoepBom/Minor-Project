using UnityEngine;
using System.Collections;

public class PhilMovement : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
    private Quaternion Rotation;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
   
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        // movement
        Vector3 movement = new Vector3(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
        
        // turning
        if (movement == new Vector3(0, 0, 0))
        {
            rb.MoveRotation(Rotation);
        }
        else
        {
            Rotation = Quaternion.LookRotation(movement.normalized);
            rb.MoveRotation(Rotation);
        }
        

    }
   
}
