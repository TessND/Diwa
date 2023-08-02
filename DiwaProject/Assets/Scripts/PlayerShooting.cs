using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject _gun;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Transform _rbFireCenter;

    Vector2 _mouse;
    float _lastShotTime;
    int _ammoCount;
    int _ammo;
    bool _shot;
    bool _reload;

    public static PlayerShooting Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        _ammo = Gun.Instance.FinalAmmo;
        _ammoCount = _ammo;
    }

    // Update is called once per frame
    void Update()
    {
        MouseMove();

        if (_ammo != Gun.Instance.FinalAmmo)
            CheckAmmo();

        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= _lastShotTime + _gun.GetComponent<Gun>().FinalShotDelay + PlayerParametrs.Instance.MainShotDelay && _ammoCount != 0)
        {
            Shot();
        }
        else if (_ammoCount == 0)
        {
            Reload();
        }

        if (Gun.Instance.FinalBurst > 1 && Time.time >= _lastShotTime + Gun.Instance.FinalBurstDelay * Gun.Instance.FinalBurst)
            Gun.Instance.BurstStop();
        Gun.Instance.Reload = false; //решить

    }

    public void ChangeGun(GameObject Gun)
    {
        _gun = Gun;
    }

    private void CheckAmmo()
    {
        _ammo = Gun.Instance.FinalAmmo;
        _ammoCount = _ammo;
    }

    private void MouseMove()
    {
        _mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Vector2.SignedAngle(Vector2.up, _mouse - _rb.position);

        _rbFireCenter.localEulerAngles = new Vector3(0, 0, angle);
    }

    public void Shot()
    {
        Gun.Instance.Shoot();
        _ammoCount--;
        _lastShotTime = Time.time;
        Gun.Instance.Shot = true;
    }

    public void Reload()
    {
        if (Time.time - _lastShotTime >= Gun.Instance.FinalReloadTime)
        {
            _ammoCount = Gun.Instance.FinalAmmo;
            Gun.Instance.Reload = true;
        }

    }
}
