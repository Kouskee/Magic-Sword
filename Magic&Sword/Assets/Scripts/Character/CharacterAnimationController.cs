using System;
using UnityEngine;

namespace Character
{
    public class CharacterAnimationController : MonoBehaviour
    {
        private static readonly int AnimMoving = Animator.StringToHash("isMoving");
        private static readonly int AnimSpeed = Animator.StringToHash("Animation Speed");
        private static readonly int AnimVelocity = Animator.StringToHash("Velocity");

        private Animator _animator;

        private void Awake() => TryGetComponent(out _animator);

        private void Start()
        {
            _animator.SetBool(AnimMoving, true);
        }

        public void SetMove(float velocity)
        {
            _animator.SetFloat(AnimVelocity, velocity);
        }

        public void SetStrafe(bool isStrafe)
        {
            _animator.SetFloat(AnimSpeed, isStrafe ? 0 : 1);
        }

        public void SetDeath()
        {
        }
    }
}