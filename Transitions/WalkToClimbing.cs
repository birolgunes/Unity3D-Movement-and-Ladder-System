using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine.Abstract;
using MovementSystem.States;

namespace MovementSystem.Transitions
{
    public class WalkToClimbing : MonoBehaviour, ITransition
    {
        public IFiniteStateMachine Context { get; set; }
        public List<ITransition> Transitions { get; set; }
        public IState CurrentState { get; set; }
        public IState NextState { get; set; }
        Animator _animator;

        public bool canClimb;
        string getWhere = "";
        GameObject _Player;
        Ladder ladder;
        GameObject Ladder;
        void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }
        public void setTransition(IState currentState, IState nextState, GameObject player)
        {
            this.CurrentState = currentState;
            this.NextState = nextState;
            _Player = player;

        }

        public bool Condition()
        {
            if (Input.GetKey(KeyCode.E) && canClimb)
            {
                _Player.transform.rotation = ladder.transform.rotation;
                if (getWhere == "bot")
                {
                    _Player.transform.position = ladder.botTransform.transform.position;
                }
                else
                    _Player.transform.position = ladder.topTransform.transform.position;
                return true;
            }
            return false;
        }

        public void InitTransition()
        {
        }
        void OnTriggerEnter(Collider coll)
        {
            if (coll.CompareTag("LadderParent"))
            {
                ladder = coll.gameObject.GetComponentInParent<Ladder>();
            }
            if (coll.CompareTag("BotPlatform"))
            {
                getWhere = "bot";
                canClimb = true;
            }
            if (coll.CompareTag("TopPlatform"))
            {
                getWhere = "top";
                canClimb = true;
            }
        }

        void OnTriggerExit(Collider coll)
        {
            if (coll.CompareTag("BotPlatform") || coll.CompareTag("TopPlatform"))
            {
                canClimb = false;
            }
        }
    }
}