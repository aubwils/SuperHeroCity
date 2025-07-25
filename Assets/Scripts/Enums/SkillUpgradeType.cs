using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillUpgradeType
{
    none,

    // --------- Dash Upgrades ---------
    Dash, // Basic Dash
    Dash_CloneOnStart, // Clone appears when dash starts
    Dash_CloneOnStartAndEnd, // Clone appears when dash starts and ends
    Dash_SmashOnEnd, // AOE Smash damage when dash ends
    Dash_SmashOnEndAndStart, // AOE Smash damage when dash starts and ends

    // --------- Mini Bot Tree ---------
    MiniBot, // minibot explodes by time or touch
    MiniBot_MoveToEnemy, // minibot moves towrads nearest enemy
    MiniBot_MultiShot, // can have x minibots and can cast them all in a row 

    // --------- Teleoprt Tree ---------
    Portal, // Creat a Portal
    Portal_Teleport, // swap places with portal
    Portal_TeleportAndHeal // swap and hp is back to % when placed
}
