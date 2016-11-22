using UnityEngine;
using System.Collections;

public class MoveonPath : MonoBehaviour
{
    public EditorPath pathToFolow;

    public int currentWayPointID = 0;
    public float speed = 2.0f;
    public float rotationSpeed = 5.0f;

    private float reachDistance = 1.0f; //difference between the centre of the enemy and the point created by the EditorPath
    public string pathName;

    Vector3 current_position;
    Vector3 last_position;
	// Use this for initialization
	void Start ()
    {
        //pathToFolow = GameObject.Find(pathName).GetComponent<EditorPath>();
        last_position = transform.position;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector3.Distance(pathToFolow.path_objects[currentWayPointID].position, transform.position);
        transform.position= Vector3.MoveTowards(transform.position, pathToFolow.path_objects[currentWayPointID].position, Time.deltaTime * speed); //Move from current position to next position

        Quaternion rotation = Quaternion.LookRotation(pathToFolow.path_objects[currentWayPointID].position - transform.position); // position we are going to minus the position we are looking at
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        if (distance <= reachDistance)
        {
            currentWayPointID++;
        }

        if(currentWayPointID >= pathToFolow.path_objects.Count)
        {
            currentWayPointID = 0;
        }
	}
}
