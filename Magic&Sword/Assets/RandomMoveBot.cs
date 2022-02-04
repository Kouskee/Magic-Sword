using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoveBot : MonoBehaviour
{
    private CharacterController _controller;

    private Vector3 _moveDirection;

    private float _time;
    

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        if (_time <= 0)
        {
            var moveDirection = Random.insideUnitSphere * 2f;
            _moveDirection = new Vector3(moveDirection.x, 0f, moveDirection.z);
            _time = 1f;
        }
        else
        {
            _time -= Time.deltaTime;
        }

        _controller.Move(_moveDirection * Time.deltaTime * 2f);
    }
}
