using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine.Abstract;

namespace MovementSystem.States
{
    public class PlayerWalk : IState
    {
        [SerializeField] float movementSpeed = 2.5f;
        GameObject _Player;
        GameObject _Ladder;
        Animator _animator;
        Vector3 _forward, _right;
        PlayerClimb playerClimb;
        public IFiniteStateMachine Context { get; set; }
        public List<ITransition> Transitions { get; set; }
        Vector3 movement;

        public PlayerWalk(GameObject Player, Vector3 forward, Vector3 right, Animator animator)
        {
            Transitions = new List<ITransition>();
            _Player = Player;
            _forward = forward;
            _right = right;
            _animator = animator;
        }

        public void OnExit()
        {
        }

        public void OnStart()
        {
        }

        public void Tick()
        {
            movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            // W A S D tuşları basılıyor iken hareketi sağlasın, "Walk" animasyonuna geçilsin, CTRL Eğilmeyi Sağlasın
            if (movement.magnitude > 0.1) // isWalking gibi bir şeyle düzenlenecek
            {
                Move();
            }
            Animate();
        }
        void Move()
        {
            //Vector3 rightMovement = _right.normalized * movementSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            //Vector3 upMovement = _forward.normalized * movementSpeed * Time.deltaTime * Input.GetAxis("Vertical");
            Vector3 heading = Vector3.Normalize(movement);
            _Player.transform.forward = heading;
            _Player.transform.position += movement* Time.deltaTime * movementSpeed;
            //_Player.transform.position += upMovement;
        }

        void Animate()
        {

            //SpeedControl
            if (movement.magnitude > 1)
            {
                _animator.SetFloat("Blend", 1);
            }
            else
            {
                _animator.SetFloat("Blend", movement.magnitude);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                movementSpeed = 1.5f;
                _animator.SetBool("isSneak", true);
                _animator.SetFloat("Sneakblend", (movement.magnitude));
            }
            else
            {
                movementSpeed = 2.5f;
                _animator.SetBool("isSneak", false);
                _animator.SetFloat("Sneakblend", (movement.magnitude));
            }
        }

    }
}
