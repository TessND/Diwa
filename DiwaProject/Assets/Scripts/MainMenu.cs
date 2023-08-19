using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject _menu;
    [SerializeField] GameObject _gallery;

    bool _isMenu = true;

    private void Start()
    {
        _menu.SetActive(true);
        _gallery.SetActive(false);
    }

    public void ChangeCanvas()
    {
        if (_isMenu)
        {
            _menu.SetActive(false);
            _gallery.SetActive(true);
            _isMenu = false;
        }
        else
        {
            _menu.SetActive(true);
            _gallery.SetActive(false);
            _isMenu = true;
        }
    }
}
