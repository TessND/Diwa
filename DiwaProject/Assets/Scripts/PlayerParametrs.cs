using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerParametrs : MonoBehaviour
{
    [SerializeField] public GameObject item;
    [SerializeField] public float MoveSpeed;
    [SerializeField] public float MoveLength;
    [SerializeField] public float MoveKD;
    [SerializeField] public float MainShotDelay;
    [SerializeField] public float Defence;
    [SerializeField] public float CritDamage;
    [SerializeField] public float CritChance;
    [SerializeField] public float Vampirism;
    [SerializeField] public float BodyDamage;
    [SerializeField] public float Health;
    [SerializeField] public float TimeOfInvincibility;
    [SerializeField] public float SmoothTime;

    [SerializeField] public float Experience;
    [SerializeField] public float ExperienceMultiplier;
    [SerializeField] public float Level;
    [SerializeField] Image _expBar;
    [SerializeField] TextMeshProUGUI _lvlBar;
    [SerializeField] float _expBarSize;
    [SerializeField] float _levelStartSize;
    [SerializeField] float _levelSize;
    float _levelSizeMultiplier;
    float _lateExp = 0;


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


    [SerializeField] SpriteRenderer _obody;
    [SerializeField] SpriteRenderer _olight;
    [SerializeField] SpriteRenderer _obodyBorder;

    Color _bodyAlpha;
    Color _lightAlpha;
    Color _bodyBorderAlpha;
    Color _body;
    Color _light;
    Color _bodyBorder;

    public float GettingDamage;
    public float OldHP;

    public static PlayerParametrs Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else Destroy(gameObject);

        OldHP = PlayerParametrs.Instance.Health;

        _bodyAlpha = new Color(_obody.color.r, _obody.color.g, _obody.color.b, _obody.color.a / 2);
        _lightAlpha = new Color(_olight.color.r, _olight.color.g, _olight.color.b, _olight.color.a / 2);
        _bodyBorderAlpha = new Color(_olight.color.r, _obodyBorder.color.g, _obodyBorder.color.b, _obodyBorder.color.a / 2);
        _body = _obody.color;
        _light = _olight.color;
        _bodyBorder = _obodyBorder.color;

        _levelSize = _levelStartSize;
        _expBarSize = 800 / _levelSize;
        _levelSizeMultiplier = 1;
        _lvlBar.text = "LVL" + Level;
    }

    private void Update()
    {
        if (Health > 0)
        {
            CheckHP();
            LevelProgress();
        }

        if (Health <= 0)
            Die();

        if (Input.GetKeyDown(KeyCode.F))
            PlayerBehaviour.Instance.Alive();

        if (Input.GetKeyDown(KeyCode.K))
            item.transform.parent = PlayerBehaviour.Instance.gameObject.transform.GetChild(1);
    }

    private void CheckHP()
    {
        if (Health < OldHP)
        {
            VisualiseInvincibility();
        }
        else if (Health > OldHP)
            OldHP = Health;
    }

    private void VisualiseInvincibility()
    {
        if (Time.time > GettingDamage + TimeOfInvincibility)
        {
            _obody.color = _body;
            _olight.color = _light;
            _obodyBorder.color = _bodyBorder;
            OldHP = Health;
        }
        else
        {
            _obody.color = _bodyAlpha;
            _olight.color = _lightAlpha;
            _obodyBorder.color = _bodyBorderAlpha;
        }
    }

    private void Die()
    {
        PlayerBehaviour.Instance.gameObject.SetActive(false);
    }

    private void LevelProgress()
    {
        if (_expBarSize * Experience > 800)
        {
            GainExpAfterLvlUp();
        }

        if (Experience != 0)
            _expBar.rectTransform.sizeDelta = new Vector2(_expBarSize * Experience, _expBar.rectTransform.sizeDelta.y);

        if (_expBar.rectTransform.sizeDelta.x == 800)
        {
            _expBar.rectTransform.sizeDelta = new Vector2(0, _expBar.rectTransform.sizeDelta.y);
            LevelScale();
            _expBarSize = 800 / _levelSize;
            ++Level;
            _lvlBar.text = "LVL " + Level;

            if (_lateExp != 0)
            {
                GiveExpAfterLvlUp();
            }
            Experience = 0;

        }
    }

    private void LevelScale()
    {
        if (Level % 2 == 0)
            if (Level > 2)
                ++_levelSizeMultiplier;
        _levelSize += _levelSizeMultiplier;
    }

    private void GainExpAfterLvlUp()
    {
        _lateExp = ((_expBarSize * Experience) - 800) / _expBarSize;
        Experience -= _lateExp;
    }

    private void GiveExpAfterLvlUp()
    {
        _expBar.rectTransform.sizeDelta = new Vector2(_expBarSize * _lateExp, _expBar.rectTransform.sizeDelta.y);
        _lateExp = 0;
    }
}
