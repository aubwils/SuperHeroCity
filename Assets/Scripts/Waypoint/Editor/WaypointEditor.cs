using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
  private Waypoint WaypointTarget => target as Waypoint;

    private void OnSceneGUI()
    {
        if (WaypointTarget.Waypoints == null || WaypointTarget.Waypoints.Length == 0)
            {
                //Debug.Log("No waypoints to display.");
                return;
            }

        //Debug.Log($"Displaying {WaypointTarget.Waypoints.Length} waypoints.");
        Handles.color = Color.red;
        for (int i = 0; i < WaypointTarget.Waypoints.Length; i++)
        {
            EditorGUI.BeginChangeCheck(); // Check if the user is changing the position of the waypoint

            Vector3 currentWaypoint = WaypointTarget.EntityPosition + WaypointTarget.Waypoints[i]; // Get the current waypoint position
            Vector3 newPosition = Handles.FreeMoveHandle(currentWaypoint, 0.5f, Vector3.one * 0.5f, Handles.SphereHandleCap); // FreeMoveHandle is used to move the waypoint in the scene view

            GUIStyle text = new GUIStyle();
            text.fontStyle = FontStyle.Bold;
            text.fontSize = 16;
            text.normal.textColor = Color.black;
            Vector3 textPosition = new Vector3(0.2f, -0.2f);
            Handles.Label(WaypointTarget.EntityPosition + WaypointTarget.Waypoints[i] + textPosition, $"{i + 1}", text); // Label the waypoint with its index

            if(EditorGUI.EndChangeCheck()) // If the user is changing the position of the waypoint
            {
                Undo.RecordObject(target, "Free Move"); // Record the change for undo
                WaypointTarget.Waypoints[i] = newPosition - WaypointTarget.EntityPosition; // Update the waypoint position
            }
        }
    }

}
