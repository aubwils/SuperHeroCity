using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//only use for quick prototypeing...
public class AnimationRelay : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    /// <summary>
    /// Called from Animation Events, passing the event name as a string.
    /// </summary>
    /// <param name="eventName">The name of the event to relay</param>
    public void RelayEvent(string eventName)
    {
            Debug.Log($"RelayEvent called with eventName: {eventName}");

        if (player == null)
        {
            Debug.LogWarning("No Player found for AnimationRelay!");
            return;
        }

        // Use C# Reflection to call method dynamically
        var method = player.GetType().GetMethod(eventName);
        if (method != null)
        {
            method.Invoke(player, null);
        }
        else
        {
            Debug.LogWarning($"Player does not have a method called '{eventName}'");
        }
    }
}
