using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] public GameObject Bullet;

    [SerializeField] float _shotDelay;
    [SerializeField] float _damage;
    [SerializeField] float _bulletLife;
    [SerializeField] float _rangeAngle;
    [SerializeField] float _splash;
    [SerializeField] float _reloadTime;
    [SerializeField] float _burstDelay;
    [SerializeField] float _bulletSpeed;

    [SerializeField] int _ammo;
    [SerializeField] int _burst;


    public float FinalShotDelay;
    public float FinalDamage;
    public float FinalBulletLife;
    public float FinalRangeAngle;
    public float FinalSplash;
    public float FinalReloadTime;
    public float FinalBurstDelay;
    public float FinalBulletSpeed;

    public int FinalAmmo;
    public int FinalBurst;

    public bool ChangingParametrs;
    public bool Reload;
    public bool Shot;

    private Transform _rbFirePoint;


    public static Gun Instance;


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
    private void Start()
    {
        _rbFirePoint = GameObject.FindGameObjectWithTag("Fire Point").GetComponent<Transform>();
        UpdateStatesOfWeapon();
    }

    private void Update()
    {
        if (ChangingParametrs)
            UpdateStatesOfWeapon();
    }

    public void UpdateStatesOfWeapon()
    {
        FinalShotDelay = PlayerParametrs.Instance.WeaponShotDelay + _shotDelay;
        FinalDamage = PlayerParametrs.Instance.WeaponDamage + _damage;
        FinalBulletLife = PlayerParametrs.Instance.WeaponBulletLife + _bulletLife;
        FinalRangeAngle = PlayerParametrs.Instance.WeaponRangeAngle + _rangeAngle;
        FinalSplash = PlayerParametrs.Instance.WeaponSplash + _splash;
        FinalReloadTime = PlayerParametrs.Instance.WeaponReloadTime + _reloadTime;
        FinalBurstDelay = PlayerParametrs.Instance.WeaponBurstDelay + _burstDelay;
        FinalBulletSpeed = PlayerParametrs.Instance.WeaponBulletSpeed + _bulletSpeed;

        FinalAmmo = PlayerParametrs.Instance.WeaponAmmo + _ammo;
        FinalBurst = PlayerParametrs.Instance.WeaponBurst + _burst;
    }

    public void Shoot()
    {
        if (Bullet && FinalBurst <= 1)
            Instantiate(Bullet, _rbFirePoint.position, _rbFirePoint.rotation);
        else if (Bullet)
            InvokeRepeating("ShootBurst", 0, FinalBurstDelay);
    }

    public void BurstStop()
    {
        CancelInvoke();
    }

    private void ShootBurst()
    {
        Instantiate(Bullet, _rbFirePoint.position, _rbFirePoint.rotation);
    }

}
