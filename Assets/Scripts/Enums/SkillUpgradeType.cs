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
    Portal_TeleportAndHeal, // swap and Heal a % back OR Teleport & Heal back to what the player was when they placed it (could be more or less health) and have it be like a time rewind ability (or BOTH one for a mutant and one for a gadget, gadget could heal and teleport and mutant could go back in "time" sort of, though only they are and no changes to the world just them )
    Portal_TeleportAndAOEBehind, // teleport and do aoe damage behind
    Portal_TeleportAndAOEArrival // teleport and do AOE damage on arrival
}
