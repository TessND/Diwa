using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] Rigidbody2D _rb;
    private Vector2 _moveVector;
    private Vector2 _moveDirection;
    private Vector2 _oldplayerPosition;
    private Vector2 _newplayerPosition;
    private Vector2 _eps;
    private float _distance1;
    private float _distance2;

    [SerializeField] float _moveSpeed;
    [SerializeField] float _moveLength;
    [SerializeField] float _mainShotDelay;
    [SerializeField] float _defence;
    [SerializeField] float _critDamage;
    [SerializeField] float _critChance;
    [SerializeField] float _vampirism;
    [SerializeField] float _bodyDamage;
    [SerializeField] float _health;

    [SerializeField] float _smoothTime;
    [SerializeField] float _smoothRotationTime;

    public float FinalMoveSpeed;
    public float FinalMoveLength;
    public float FinalMainShotDelay;
    public float FinalDefence;
    public float FinalCritDamage;
    public float FinalCritChance;
    public float FinalVampirism;
    public float FinalBodyDamage;
    public float FinalHealth;

    public bool ChangingParametrs;

    void Start()
    {
        UpdateStatesOfEnemy();
        _moveDirection = Vector2.zero;
        _rb.velocity = Vector2.zero;
        _eps = new Vector2(0.1f, 0.1f);
    }

    private void FixedUpdate()
    {
        if (ChangingParametrs)
            UpdateStatesOfEnemy();

        if (PlayerBehaviour.Instance.gameObject.activeSelf)
            Move();

        StopRotate();
    }

    private void Move()
    {
        _oldplayerPosition = PlayerBehaviour.Instance.gameObject.transform.localPosition;

        _moveVector = (_oldplayerPosition - _rb.position).normalized * _moveSpeed;

        if (StraightVector() && _rb.velocity.magnitude > 1f && PlayerPosition())
                _rb.velocity = Vector2.SmoothDamp(_rb.velocity, Vector2.zero, ref _moveDirection, _smoothTime);
        else _rb.velocity += (_moveVector);

        _newplayerPosition = PlayerBehaviour.Instance.gameObject.transform.localPosition;
    }

    private bool StraightVector()
    {
        bool plusY = _rb.velocity.normalized.y > (_oldplayerPosition - _rb.position).normalized.y + 0.5f;
        bool minusY = _rb.velocity.normalized.y < (_oldplayerPosition - _rb.position).normalized.y - 0.5f;
        bool plusX = _rb.velocity.normalized.x > (_oldplayerPosition - _rb.position).normalized.x + 0.5f;
        bool minusX = _rb.velocity.normalized.x < (_oldplayerPosition - _rb.position).normalized.x - 0.5f;
        bool plusplusXY = plusX && plusY;
        bool minusminusXY = minusX && minusY;
        bool plusminusXY = plusX && minusY;
        bool plusminusYX = plusY && minusX;
        if (plusplusXY || minusminusXY || plusminusXY || plusminusYX || plusX || plusY || minusX || minusY)
            return true;
        else return false;
    }

    private bool PlayerPosition()
    {
        bool vel = PlayerBehaviour.Instance.gameObject.GetComponent<Rigidbody2D>().velocity == Vector2.zero;
        bool pos = _oldplayerPosition == _newplayerPosition;
        if (vel && pos)
            return true;
        else return false;
    }

    private void StopRotate()
    {
        if (_rb.angularVelocity != 0f)
            _rb.angularVelocity = Mathf.MoveTowardsAngle(_rb.angularVelocity, 0, _smoothRotationTime);
    }

    public void UpdateStatesOfEnemy()
    {
        FinalMoveSpeed = _moveSpeed + EnemyParametrs.Instance.MoveSpeed;
        FinalMoveLength = _moveLength + EnemyParametrs.Instance.MoveLength;
        FinalMainShotDelay = _mainShotDelay + EnemyParametrs.Instance.MainShotDelay;
        FinalDefence = _defence + EnemyParametrs.Instance.Defence;
        FinalCritDamage = _critDamage + EnemyParametrs.Instance.CritDamage;
        FinalCritChance = _critChance + EnemyParametrs.Instance.CritChance;
        FinalVampirism = _vampirism + EnemyParametrs.Instance.Vampirism;
        FinalBodyDamage = _bodyDamage + EnemyParametrs.Instance.BodyDamage;
        FinalHealth = _health + EnemyParametrs.Instance.Health;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        if (playerMovement)
        {

            float impactDamage = 0;

            if (PlayerParametrs.Instance.Health == PlayerParametrs.Instance.OldHP)
            {
                impactDamage = Crit();

                PlayerParametrs.Instance.GettingDamage = Time.time;

                PlayerParametrs.Instance.Health -= impactDamage - PlayerParametrs.Instance.Defence;
            }

            if (PlayerParametrs.Instance.Health < PlayerParametrs.Instance.OldHP)
                Vampiric(impactDamage);
        }
    }

    private float Crit()
    {
        if (Random.value <= FinalCritChance / 100)
            return FinalBodyDamage + FinalBodyDamage * (FinalCritDamage / 100);
        else
            return FinalBodyDamage;
    }

    private void Vampiric(float impactDamage)
    {
        FinalHealth += impactDamage * (FinalVampirism / 100);
    }
}
