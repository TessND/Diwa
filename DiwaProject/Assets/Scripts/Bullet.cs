using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _bulletMove;
    [SerializeField] GameObject _exp;

    private void Start()
    {
        _bulletMove = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_bulletMove)
            _bulletMove.velocity = (transform.up * Gun.Instance.FinalBulletSpeed);
        Destroy(gameObject, Gun.Instance.FinalBulletLife);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBehaviour enemyBehaviour = collision.GetComponent<EnemyBehaviour>();
        if (enemyBehaviour)
        {
            float impactDamage = 0;

            impactDamage = Crit();

            enemyBehaviour.FinalHealth -= impactDamage - enemyBehaviour.FinalDefence;

            Vampiric(impactDamage);

            if (enemyBehaviour.FinalHealth <= 0)
            {
                Destroy(collision.gameObject);
                Instantiate(_exp,collision.gameObject.transform.position, Quaternion.identity);
            }
        }

        Destroy(gameObject);
    }

    private float Crit()
    {
        if (Random.value <= PlayerParametrs.Instance.CritChance / 100)
            return Gun.Instance.FinalDamage + Gun.Instance.FinalDamage * (PlayerParametrs.Instance.CritDamage / 100);
        else
            return Gun.Instance.FinalDamage;

    }

    private void Vampiric(float impactDamage)
    {
        PlayerParametrs.Instance.Health += impactDamage * (PlayerParametrs.Instance.Vampirism / 100);
    }
}
