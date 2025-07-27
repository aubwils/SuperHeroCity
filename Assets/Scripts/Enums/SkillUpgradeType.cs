using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillUpgradeType
{
    None,

    // --------- Dash Upgrades ---------
    Dash, // Basic Dash
    Dash_CloneOnStart, // Clone appears when dash starts
    Dash_CloneOnStartAndEnd, // Clone appears when dash starts and ends
    Dash_SmashOnEnd, // AOE Smash damage when dash ends
    Dash_SmashOnEndAndStart, // AOE Smash damage when dash starts and ends

    // --------- Deployable Damage Tree --------- (e.g. Mini Bots, Magic something, other?)
    DeployableBomb, // A Deployable Bomb is created and explodes by time or touch
    DeployableBomb_MoveToEnemy, // Bomb moves towrads nearest enemy
    DeployableBomb_MultiDeploy, // Deploy multiple bombs in a row

    // --------- Teleoprt Tree ---------
    Portal, // Creat a Portal
    Portal_Teleport, // swap places with portal
    Portal_TeleportAndHeal // swap and hp is back to % when placed
}
