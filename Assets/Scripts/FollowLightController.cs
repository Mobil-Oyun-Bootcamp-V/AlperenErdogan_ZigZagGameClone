using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Player;
using UnityEngine;

public class FollowLightController : MonoBehaviour
{
    private Vector3 _targetPosition;
    private float _height;
    private float _speed;
    private bool _once = false;
    private void Start()
    {
        _speed = GameManager.Instance.gameSpeed;
        _height = transform.position.y;
    }

    private void Update()
    {
        if (!GameManager.Instance.isGameStarted)
            return;
        if (!_once)
        {
            Vector2 appliedPosition2D=PathManager.Instance.LightPathQueue.Peek();
            _targetPosition = new Vector3(appliedPosition2D.x, _height, appliedPosition2D.y);
            _once = true;
        }
        if (_speed != GameManager.Instance.gameSpeed)
            _speed = GameManager.Instance.gameSpeed;
        Move();
    }

    public void DequeueDirection()
    {
        //If the light lagged behind, directly move to target position
        if (transform.position.x > _targetPosition.x || transform.position.z < _targetPosition.z)
        {
            transform.position = _targetPosition;
        }
        //Dequeuing next direction from queue to moving light
        Vector2 appliedPosition2D=PathManager.Instance.LightPathQueue.Dequeue();
        _targetPosition = new Vector3(appliedPosition2D.x, _height, appliedPosition2D.y);
    }

    private void Move()
    {
        //Moving light
        transform.position=Vector3.MoveTowards(transform.position,_targetPosition,Time.deltaTime*_speed);
    }
}   