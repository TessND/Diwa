using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject _menu;
    [SerializeField] GameObject _gallery;

    bool _isMenu = true;

    private void Start()
    {
        if (GameManager.Instance.CurrentScene == 0)
        {
            _menu.SetActive(true);
            _gallery.SetActive(false);
        }
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

    public void ChangeScene()
    {
        GameManager.Instance.StartCoroutine("GameSceneChange");
    }

    public void StartGame()
    {
        GameManager.Instance.ShuffleCards();
    }
}
