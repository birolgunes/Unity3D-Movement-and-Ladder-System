using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine.Abstract;
using MovementSystem.States;

namespace MovementSystem.Transitions
{
    public class ClimbingToWalk : MonoBehaviour, ITransition
    {
        public IFiniteStateMachine Context { get; set; }
        public List<ITransition> Transitions { get; set; }
        public IState CurrentState { get; set; }
        public IState NextState { get; set; }
        Animator _animator;

        public bool canWalk;

        GameObject _Player, _Ladder;
        Ladder ladder;
        void Awake()
        {
        }

        public void setTransition(IState currentState, IState nextState, GameObject player, Animator animator)
        {
            this.CurrentState = currentState;
            this.NextState = nextState;
            _Player = player;
            _animator = animator;
        }

        public bool Condition()
        {
            return canWalk || Input.GetKey(KeyCode.Space) || PositionControl();
        }

        public void InitTransition()
        {
        }

        bool PositionControl()
        {
            return (_Player.transform.position.y + 0.752) < ladder.BotDrop.transform.position.y;
        }

        void OnTriggerEnter(Collider coll)
        {
            if (coll.CompareTag("LadderParent"))
            {
                ladder = coll.gameObject.GetComponentInParent<Ladder>();
            }

            if (coll.CompareTag("TopDrop") && Input.GetKey(KeyCode.W))
            {
                _Player.transform.position = ladder.TopPlatform.transform.position;
                canWalk = true;
            }
            if (coll.CompareTag("BotDrop") && Input.GetKey(KeyCode.S))
            {
                canWalk = true;
            }
            if (coll.CompareTag("TopPlatform") || coll.CompareTag("BotPlatform"))
            {
                canWalk = false;
            }
        }
    }
}