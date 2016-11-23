using UnityEngine;
using System.Collections;

public class PickUpMovement : MonoBehaviour
{
    private Vector3 movement;

	// Use this for initialization
	void Start ()
    {
        movement = new Vector3(15, 30, 45);
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(movement * Time.deltaTime);
	}
}
