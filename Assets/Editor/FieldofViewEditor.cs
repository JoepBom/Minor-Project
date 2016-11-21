using UnityEngine;
using System.Collections;
using UnityEditor;
using Mono.CSharp;

[CustomEditor (typeof (FieldOfView))]
public class FieldofViewEditor : Editor
{
    void OnSceneGUI()
    {
        FieldOfView fow = (FieldOfView) target;
        //color of the circle
        Handles.color = Color.white;
        //Draw a circle arround the player with radius viewRadius
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);

        //The range in which the player can see, with viewAngleA as the most left sight and viewAngleB as the most right sight
        Vector3 viewAngleA = fow.DirectionFromAngle((-fow.viewAngle/2), false);
        Vector3 viewAngleB = fow.DirectionFromAngle(( fow.viewAngle/2), false);

        //Draws a line at an angle of -viewAngle/2 from the player centre to the radius
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        //Draws a line at an angle of +viewAngle/2 from the player centre to the radios
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in fow.visibleTargets)
        {
            Handles.DrawLine(fow.transform.position, visibleTarget.position);
        }
    }
}
