using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string isWalk = "isWalk";
    private const string AttackTrigger = "AttackTrigger";
    private const string DamageTrigger = "DamageTrigger";

    [SerializeField] private Animator player_Animator;

    public void WalkAnim(bool flag)
    {
        player_Animator.SetBool(isWalk, flag);
    }

    public void AttackAnim()
    {
        player_Animator.SetTrigger(AttackTrigger);
    }

    public void StopAnim()
    {
        player_Animator.SetTrigger(DamageTrigger);
    }
}
