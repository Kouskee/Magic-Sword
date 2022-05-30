using System;
using UnityEngine;

namespace Character
{
    public class CharacterAnimationController : MonoBehaviour
    {
        private static readonly int AnimSpeed = Animator.StringToHash("Speed");

        private Animator _animator;

        private void Awake() => TryGetComponent(out _animator);

        public void SetMove(float velocity)
        {
            _animator.SetFloat(AnimSpeed, velocity);
        }

        public void SetStrafe(bool isStrafe)
        {
            _animator.speed = isStrafe ? 0 : 1;
        }

        public void SetDeath()
        {
        }
    }
}