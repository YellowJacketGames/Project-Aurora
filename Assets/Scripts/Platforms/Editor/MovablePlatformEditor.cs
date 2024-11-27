using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MovablePlatform))]
public class MovablePlatformEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MovablePlatform platform = (MovablePlatform)target;

        platform.movementType =
            (MovablePlatform.MovementType)EditorGUILayout.EnumPopup("Movement Type", platform.movementType);
        platform.speed = EditorGUILayout.FloatField("Speed", platform.speed);

        switch (platform.movementType)
        {
            case MovablePlatform.MovementType.Linear:
                platform.pointA =
                    (Transform)EditorGUILayout.ObjectField("Point A", platform.pointA, typeof(Transform), true);
                platform.pointB =
                    (Transform)EditorGUILayout.ObjectField("Point B", platform.pointB, typeof(Transform), true);
                break;

            case MovablePlatform.MovementType.Circular:
                platform.circularCenter = (Transform)EditorGUILayout.ObjectField("Circular Center",
                    platform.circularCenter, typeof(Transform), true);
                platform.radius = EditorGUILayout.FloatField("Radius", platform.radius);
                break;

        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(platform);
        }
    }
}