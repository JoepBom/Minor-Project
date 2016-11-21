using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView  : MonoBehaviour
{
    //viewRadius of the Player
    public float viewRadius;
    //viewAngle of the Player
    [Range (0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    void Start()
    {
        //The execution of a coroutine can be paused at any point using the yield statement. 
        //The yield return value specifies when the coroutine is resumed.
        StartCoroutine("FindTargetWithDelay", .2f); 
    }

    /*
     * IEnumerable Interface exposes an enumerator, which supports a simple iteration over a non-generic collection.
     * The "for each" method can be used to iterate through the whole collection
     * When using the yield keyword in a statement, you indicate that the method, operator, or get accessor in which it appears is an iterator
     * 
     */
    IEnumerable FindTargetWithDelay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    void FindVisibleTargets()
    {
        //Removes all the elements that are currently in the list of visibletargets
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for(int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            //if true --> the target is within the viewAngle of the player
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle /2)
            {
                //Distance between the target and the player
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                //Checks whether there is an object between the player and the enenmy
                if(!Physics.Raycast(transform.position, dirToTarget,dstToTarget,obstacleMask))
                {
                    //if no obstacle is found --> add the target to the list of found targets
                    visibleTargets.Add(target);
                }
            }
        }
    }
    /*
     * This method decides in which direction the player is looking
     * 
     * @return the positon on the radius in which the player is looking
     */
    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0 ,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
