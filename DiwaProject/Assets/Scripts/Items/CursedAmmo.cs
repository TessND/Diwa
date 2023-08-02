using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedAmmo : MonoBehaviour
{
    public string ItemName;
    public Sprite Icon;
    [TextArea] public string Description;

    void Update()
    {
        if (PlayerBehaviour.Instance.gameObject && Gun.Instance.Reload && transform.parent == PlayerBehaviour.Instance.gameObject.transform.GetChild(1))
            CursedReload();
    }

    private void CursedReload()
    {

        if (Random.value <= 0.5f)
        {
            ++PlayerParametrs.Instance.WeaponAmmo;
            Debug.Log("Yea");

        }
        else if (Random.value > 0.5f && Gun.Instance.FinalAmmo > 1)
        {
            Debug.Log("Nope");
            --PlayerParametrs.Instance.WeaponAmmo;

        }

        Gun.Instance.UpdateStatesOfWeapon();
    }
}
