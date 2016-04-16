using UnityEngine;
using System.Collections;
using System;

public class Cinematic : MonoBehaviour
{
    public string cinematic;

    private CharacterState characterState;
    private Animator animator;
    private CharacterState.State previousState;

    private void Awake()
    {
        characterState = GetComponent<CharacterState>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.Play(cinematic);
    }

    private void OnEnable()
    {
        previousState = characterState.state;
        characterState.SetState(CharacterState.State.Cinematic);
        animator.Play(cinematic);
    }

    private void OnDisable()
    {
        characterState.SetState(CharacterState.State.Idle);
        //characterState.SetState(previousState);
    }

    private void Update()
    {
        DisableOnAnimationEnd();
    }

    private void DisableOnAnimationEnd()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
            enabled = false;
    }
}
