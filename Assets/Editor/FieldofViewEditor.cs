using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (FieldOfView))]

public class FieldofViewEditor : Editor
{
    void OnSceneGUI()
    {
        FieldOfView fow = (FieldOfView)target;

        //Draw a white circel, with the enemy center as centerpoint and a radius of sightRadius
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360.0f, fow.sightRadius);

        Vector3 viewAngleA = fow.directionFromAngle(-1 * fow.sightAngle / 2, false);
        Vector3 viewAngleB = fow.directionFromAngle(fow.sightAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.sightRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.sightRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in fow.visibleTargets)
        {
            Handles.DrawLine(fow.transform.position, visibleTarget.position);
        }

    }  
}
