using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine.Concrete;
using FiniteStateMachine.Abstract;
using MovementSystem.States;
using MovementSystem.Transitions;


public class Player : MonoBehaviour
{
    private IFiniteStateMachine finiteStateMachine;
    Animator _animator;
    Vector3 _forward, _right;
    private void Awake()
    {
        finiteStateMachine = new FiniteStateMachine.Concrete.FiniteStateMachine();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _forward = Camera.main.transform.forward;
        _forward.y = 0;
        _forward = Vector3.Normalize(_forward);
        _right = Quaternion.Euler(new Vector3(0, 90, 0)) * _forward;
        InitMovementSystem();
    }

    private void InitMovementSystem()
    {
        PlayerWalk playerWalk = new PlayerWalk(gameObject, _forward, _right, _animator);
        PlayerClimb playerClimb = new PlayerClimb(gameObject,_animator);
        finiteStateMachine.ChangeState(playerWalk);

        WalkToClimbing walkToClimbing = gameObject.AddComponent<WalkToClimbing>();
        walkToClimbing.setTransition(playerWalk, playerClimb, gameObject);
        ClimbingToWalk climbingToWalk = gameObject.AddComponent<ClimbingToWalk>();
        climbingToWalk.setTransition(playerClimb, playerWalk, gameObject,_animator);

        playerWalk.Transitions.Add(walkToClimbing);
        playerClimb.Transitions.Add(climbingToWalk);

    }

    private void Update()
    {
        finiteStateMachine.Tick();
        finiteStateMachine.CheckTransitions();
    }
}
