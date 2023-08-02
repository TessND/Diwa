using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParametrs : MonoBehaviour
{
    [SerializeField] public float MoveSpeed;
    [SerializeField] public float MoveLength;
    [SerializeField] public float MainShotDelay;
    [SerializeField] public float Defence;
    [SerializeField] public float CritDamage;
    [SerializeField] public float CritChance;
    [SerializeField] public float Vampirism;
    [SerializeField] public float BodyDamage;
    [SerializeField] public float Health;


    [SerializeField] public float WeaponShotDelay;
    [SerializeField] public float WeaponDamage;
    [SerializeField] public float WeaponBulletLife;
    [SerializeField] public float WeaponRangeAngle;
    [SerializeField] public float WeaponSplash;
    [SerializeField] public float WeaponReloadTime;
    [SerializeField] public float WeaponBurstDelay;
    [SerializeField] public float WeaponBulletSpeed;

    [SerializeField] public int WeaponAmmo;
    [SerializeField] public int WeaponBurst;

    bool _ChangingParametrs;

    public static EnemyParametrs Instance;

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
}
