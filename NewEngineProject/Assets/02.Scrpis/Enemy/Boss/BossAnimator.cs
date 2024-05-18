using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationState
{
    Idle,
    Hit,
    Attack,
    Skill,
    Run
}
public class BossAnimator : MonoBehaviour
{
    private Animator animator;
    private Dictionary<AnimationState, string> animationTriggers;
    private Dictionary<AnimationState, bool> animationStates;

    void Start()
    {
        animator = GetComponent<Animator>();
        InitializeAnimationTriggers();
        InitializeAnimationStates();
    }

    void InitializeAnimationTriggers()
    {
        animationTriggers = new Dictionary<AnimationState, string>()
        {
            { AnimationState.Idle, "IsIdle" },
            { AnimationState.Hit, "IsHit" },
            { AnimationState.Attack, "IsAttack" },
            { AnimationState.Skill, "IsSkill" },
            { AnimationState.Run, "IsRun" }
        };
    }

    void InitializeAnimationStates()
    {
        animationStates = new Dictionary<AnimationState, bool>()
        {
            { AnimationState.Idle, false },
            { AnimationState.Hit, false },
            { AnimationState.Attack, false },
            { AnimationState.Skill, false },
            { AnimationState.Run, false }
        };
    }

    public void SetAnimationState(AnimationState state, bool value)
    {
        // 해당 상태가 활성화되는 경우
        if (value)
        {
            // 다른 상태를 비활성화
            var keys = new List<AnimationState>(animationStates.Keys);
            foreach (var currentState in keys)
            {
                if (currentState != state)
                {
                    animationStates[currentState] = false;
                    
                    animator.SetBool(animationTriggers[currentState], false);
                }
            }
        }
        animationStates[state] = value;

        // 애니메이션 업데이트
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        // 상태에 따른 애니메이션 처리
        foreach (var state in animationStates)
        {
            Debug.Log(state);
            animator.SetBool(animationTriggers[state.Key], state.Value);
        }
    }
}
