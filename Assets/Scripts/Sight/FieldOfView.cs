using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{
    [Range(0,360)]
    public float sightAngle = 110.0f;
    public float sightRadius = 10.0f;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    // Use this for initialization
    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", 0.2f);
    }

    IEnumerable FindTargetsWithDelay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            findVisiblePlayer();
        }
    }
    void findVisiblePlayer()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, sightRadius, targetMask);

        for(int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            //if the angle between the point in front of the player and the target is smaller than the maximum angle / 2
            if(Vector3.Angle(transform.forward,directionToTarget) < sightAngle/2)
            {
                float distance = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position,directionToTarget, distance, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }
    
    public Vector3 directionFromAngle(float angleInDegrees, bool isGlobal)
    {
        if(!isGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
	
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
