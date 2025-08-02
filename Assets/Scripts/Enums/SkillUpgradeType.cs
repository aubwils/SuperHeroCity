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
    Portal_Teleport, // Create a portal and then swap places
    Portal_TeleportAndHeal, // Teleport & Heal a %
    Portal_TeleportTimeRewindHeal, // Teleport and Heal back to what health was before teleporting
    Portal_TeleportAndAOEBehind, // teleport and do aoe damage behind
    Portal_TeleportAndAOEArrival, // teleport and do AOE damage on arrival

    // --------- Throw Tree ---------
    ThrowabeObject, // Thow something (TEMP for starting we will throw a batarang type. weapon)
    ThrowableObject_Spin, // batarang will spin at one point and damage enemies ( thinking not at one point just on movement... tbd)
    ThrowableObject_Pierce, // Weapon will pierce X amount of targets
    ThrowableObject_Bounce // Bounce Batarang between enemies


}
