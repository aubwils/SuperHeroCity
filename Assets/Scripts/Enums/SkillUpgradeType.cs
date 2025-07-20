using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillUpgradeType
{
    // Dash Upgrades
    Dash, // Basic Dash
    Dash_CloneOnStart, // Clone appears when dash starts
    Dash_CloneOnStartAndEnd, // Clone appears when dash starts and ends
    Dash_SmashOnEnd, // AOE Smash damage when dash ends
    Dash_SmashOnEndAndStart // AOE Smash damage when dash starts and ends
   
}
