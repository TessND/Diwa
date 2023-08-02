using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Rigidbody2D _rbDotMove;
    PlayerMove _playerMove;

    Vector2 _moveVector;
    Vector2 _moveDot;
    Vector2 _stopMove;

    float _startMove;
    float _moveTime;

    private void Awake()
    {
        _playerMove = new PlayerMove();
    }

    void Start()
    {
        _moveTime = Time.time;
        _stopMove = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        DotMovement();
        PlayerSmooth();
        Move();
    }

    private void DotMovement()
    {
        _moveVector = _playerMove.Player.Movement.ReadValue<Vector2>();

        _moveDot += _moveVector * Time.deltaTime * PlayerParametrs.Instance.MoveSpeed;

        _moveDot = _rb.position + Vector2.ClampMagnitude(_moveDot - _rb.position, PlayerParametrs.Instance.MoveLength);

        _rbDotMove.MovePosition(_moveDot);
    }

    private void PlayerSmooth()
    {
        if (PlayerParametrs.Instance.Health < PlayerParametrs.Instance.OldHP)
            _startMove = Time.time;

        if (Time.time > _startMove + PlayerParametrs.Instance.SmoothTime)
            _rb.velocity = Vector2.zero;
        else
            _rb.velocity = Vector2.SmoothDamp(_rb.velocity, Vector2.zero, ref _stopMove, PlayerParametrs.Instance.SmoothTime);
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > _moveTime + PlayerParametrs.Instance.MoveKD)
        {
            _rb.velocity = Vector2.zero;
            _rb.MovePosition(_moveDot);
            _moveTime = Time.time;
        }
    }

    private void OnEnable()
    {
        _playerMove.Enable();
    }

    private void OnDisable()
    {
        _playerMove.Disable();
    }
}
