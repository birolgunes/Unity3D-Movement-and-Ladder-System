using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine.Abstract;

namespace MovementSystem.States
{
    public class PlayerClimb : IState
    {
        public IFiniteStateMachine Context { get; set; }
        public List<ITransition> Transitions { get; set; }

        GameObject _Player, _Ladder;
        Animator _animator;
        Ladder ladder;

        public PlayerClimb(GameObject player, Animator animator)
        {
            _Player = player;
            Transitions = new List<ITransition>();
            _animator = animator;
        }

        public void OnExit()
        {
            _animator.SetBool("isClimbing", false);
            //RB değişkenleri
            _Player.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            _Player.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }

        public void OnStart()
        {
            
            //animasyon başlat
            _animator.SetBool("isClimbing",true);
            // RB değişkenleri
            _Player.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            _Player.gameObject.GetComponent<Rigidbody>().useGravity = false;

        }

        public void Tick()
        {
            _animator.enabled = true;
            _animator.enabled = false;

            // W S tuşları yukarı aşağı yapsın
            if (Input.GetKey(KeyCode.W))
            {
                _animator.enabled = true;
                _Player.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * 2f);
            }
            if (Input.GetKey(KeyCode.S))
            {
                _animator.enabled = true;
                _Player.transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * 2f);
            }
        }
    }
}
