using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExpirienceOrb : MonoBehaviour
{

    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _speed;

    Vector2 _moveVector;
    Vector2 _playerPosition;
    bool _col = false;

    private void FixedUpdate()
    {
        if (_col && gameObject)
        {
            PlayerBehaviour.Instance.GainExp(_rb, _speed, _playerPosition, _moveVector);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player Radius"))
            _col = true;
        else
        {
            Destroy(gameObject);
            PlayerParametrs.Instance.Experience += 1 * PlayerParametrs.Instance.ExperienceMultiplier;
        }
    }
}
