using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _tries;
    [SerializeField] GameObject _endWindow;

    GameObject _loseWindow;
    GameObject _winWindow;

    private void Start()
    {
        _loseWindow = _endWindow.transform.GetChild(0).gameObject;
        _winWindow = _endWindow.transform.GetChild(1).gameObject;

        _endWindow.SetActive(false);
        _loseWindow.SetActive(false);
        _winWindow.SetActive(false);
    }

    private void Update()
    {
        _tries.text = "Попытки: " + GameManager.Instance.Tries;

        if (GameManager.Instance.MatchCount == 5)
            ActivateEndWindow();
        else if (GameManager.Instance.Tries <= 0)
            ActivateEndWindow();


    }

    private void ActivateEndWindow()
    {
        DeactivateCards();

        _endWindow.SetActive(true);

        if (GameManager.Instance.Tries == 0)
            _loseWindow.SetActive(true);
        else if (GameManager.Instance.MatchCount == 5)
            _winWindow.SetActive(true);
    }

    private void DeactivateCards()
    {
        foreach (var cards in GameManager.Instance.Cards)
        {
            CardHolder holder = cards.GetComponent<CardHolder>();

            holder.SpriteCard.sprite = holder.CardBack;
            holder.SpriteCard.raycastTarget = false;
            holder.MatchCode = -1;

        }
    }

    public void RepeatGame()
    {
        GameManager game = GameManager.Instance;

        game.MatchCount = 0;
        game.Tries = 5;

        game.ShuffleCards();

        _endWindow.SetActive(false);
        _loseWindow.SetActive(false);
        _winWindow.SetActive(false);


    }
}
