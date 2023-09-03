using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject _menu;
    [SerializeField] GameObject _gallery;

    GameObject _createdImage;

    bool _isMenu = true;
    bool _isClickedImage = false;
    float _clickTime = 0f;

    private void Start()
    {
        if (GameManager.Instance.CurrentScene == 0)
        {
            _menu.SetActive(true);
            _gallery.SetActive(false);
        }
    }

    private void Update()
    {
        if (_isClickedImage)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(_createdImage);
                _clickTime = Time.time;
                _isClickedImage = false;
            }
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

    public void ClickOnImage()
    {
        if (!_isClickedImage && Time.time > _clickTime + 0.1f)
        {
            var _clickedImage = EventSystem.current.currentSelectedGameObject;

            _createdImage = Instantiate(_clickedImage, _gallery.transform);
            
            var size = _createdImage.GetComponent<RectTransform>();

            size.anchoredPosition = Vector2.zero;

            float locScale = 2.1f;
            size.localScale = new Vector3(locScale, locScale, locScale);

            _createdImage.GetComponent<Image>().raycastTarget = false;

            _isClickedImage = true;
        }
    }

    public void ContinueGame()
    {
        GameManager.Instance.Tries = 2;
    }
}
