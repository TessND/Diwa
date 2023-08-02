using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public static PlayerBehaviour Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else Destroy(gameObject);
    }

    public void Alive()
    {
        if (PlayerParametrs.Instance.Health > 0)
            gameObject.SetActive(true);
    }

    public void GainExp(Rigidbody2D orb, float takeSpeed, Vector2 _playerPosition, Vector2 _moveVector)
    {
        _playerPosition = gameObject.transform.localPosition;

        _moveVector = (_playerPosition - orb.position).normalized * takeSpeed;

        orb.velocity = _moveVector;
    }
}
