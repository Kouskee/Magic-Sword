using UnityEngine;

namespace Enemy.States.Sources
{
    [CreateAssetMenu(menuName = "States/UnitStun")]
    public class UnitStun : State
    {
        private float _durationStun;

        public override void Init(float duration)
        {
            _durationStun = duration;
        }

        public override void Update()
        {
            if (_durationStun > 0)
                _durationStun -= Time.deltaTime;
            else
                IsFinished = true;
        }

        public override void Exit() => _durationStun = 0;
    }
}