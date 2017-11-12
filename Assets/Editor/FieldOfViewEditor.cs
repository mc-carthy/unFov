using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor 
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView) target;
        Handles.color = Color.white;
        Handles.DrawWireDisc(fov.transform.position ,Vector3.back, fov.viewRadius);
        Vector2 viewAngleA = fov.DirFromAngle((-fov.viewAngle/2f), false);
        Vector2 viewAngleB = fov.DirFromAngle((fov.viewAngle/2f), false);
        Handles.DrawLine(fov.transform.position, fov.transform.position + (Vector3)(viewAngleA * fov.viewRadius));
        Handles.DrawLine(fov.transform.position, fov.transform.position + (Vector3)(viewAngleB * fov.viewRadius));
    }
}
