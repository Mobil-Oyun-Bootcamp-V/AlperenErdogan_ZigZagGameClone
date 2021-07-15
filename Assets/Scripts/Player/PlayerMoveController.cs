using System;
using UnityEditor.DeviceSimulation;
using UnityEngine;
using TouchPhase = UnityEngine.TouchPhase;

namespace Player
{

    public enum Direction
    {
        Nothing,Left,Forward
    }
    public class PlayerMoveController : MonoBehaviour
    {
        private float _speed;
        private Direction _direction=Direction.Nothing;
        private Vector3 _moveDirection;
        private bool _lockInput = false;
        private float _startHeight;
        private void Start()
        {
            _startHeight = transform.position.y;
            _speed = GameManager.Instance.gameSpeed;
            GameManager.Instance.ONFailed += OnFailed;
        }
        private void Update()
        {
            if (_lockInput)
                return;
            if (_speed != GameManager.Instance.gameSpeed)
                _speed = GameManager.Instance.gameSpeed;
            if(transform.position.y<_startHeight)
                GameManager.Instance.ONFailed?.Invoke();
            MovePlayer();
        }

        private void MovePlayer()
        {
            if (Input.touchCount>0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    if (!GameManager.Instance.isGameStarted)
                    {
                        GameManager.Instance.ONGameStarted?.Invoke();
                    }
                    GameManager.Instance.ONTouch?.Invoke();
                    _direction = _direction==Direction.Forward ? Direction.Left : Direction.Forward;
                }
            }

            if (Application.isEditor&&Input.GetMouseButtonDown(0))
            {
                if (!GameManager.Instance.isGameStarted)
                {
                    GameManager.Instance.ONGameStarted?.Invoke();
                }
                GameManager.Instance.ONTouch?.Invoke();
                _direction = _direction==Direction.Forward ? Direction.Left : Direction.Forward;
            }
            switch (_direction)
            {
                case Direction.Forward:
                    _moveDirection = Vector3.forward;
                    break;
                case Direction.Left:
                    _moveDirection=Vector3.left;
                    break;
                default:
                    _moveDirection = Vector3.zero;
                    break;
            }
            _moveDirection *= (_speed * Time.deltaTime);
            transform.Translate(_moveDirection);
        }

        void OnFailed()
        {
            _lockInput = true;
            transform.GetChild(0).parent = null;
            GetComponent<Rigidbody>().AddForce(_moveDirection*10,ForceMode.Impulse);
        }
    }
}