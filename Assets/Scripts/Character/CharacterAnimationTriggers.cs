using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationTriggers : MonoBehaviour
{
    private CharacterBrain characterBrain;
    private CharacterCombat characterCombat;

    private void Awake()
    {
        characterBrain = GetComponentInParent<CharacterBrain>();
        characterCombat = GetComponentInParent<CharacterCombat>();
    }

    private void CallAnimationFinishTrigger()
    {
        characterBrain.CallAnimationFinishTrigger();
    }

     private void CallAnimationAttackTrigger()
    {
       characterCombat.PreformAttack();
    }
}
