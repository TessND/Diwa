using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    [SerializeField] Transform _player;
    [SerializeField] Transform _dot;

    [SerializeField] Rigidbody2D _rb;

    [SerializeField] float _maxDeltaX;
    [SerializeField] float _maxDeltaY;

    Vector2 _playerPosition;
    Vector2 _dotPosition;
    Vector2 _newPosition;

    void LateUpdate()
    {
        if (_player)
        {
            _playerPosition = _player.GetComponent<Rigidbody2D>().position;
            _dotPosition = _dot.GetComponent<Rigidbody2D>().position;
            _newPosition = (_dotPosition + _playerPosition)/2;
            _newPosition.x = Mathf.MoveTowards(_rb.position.x, _newPosition.x, _maxDeltaX * PlayerParametrs.Instance.MoveSpeed * PlayerParametrs.Instance.MoveLength);
            _newPosition.y = Mathf.MoveTowards(_rb.position.y, _newPosition.y, _maxDeltaY * PlayerParametrs.Instance.MoveSpeed * PlayerParametrs.Instance.MoveLength);

            _rb.MovePosition(_newPosition);
        }
    }
}
