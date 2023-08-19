using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int CardsCount;
    [SerializeField] public bool Match;
    public float SecondClick;

    [SerializeField] int _matchCode;
    [SerializeField] Sprite _cardBack;
    [SerializeField] List<Sprite> _cardFront;
    public List<GameObject> Cards;


    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else Destroy(gameObject);

    }

    void Start()
    {
        CardsCount = 0;
        Match = false;
    }

    public void NullCardsCount()
    {
        CardsCount = 0;
    }

    public void CardsManagment()
    {
        foreach (var card in Cards)
        {
            card.GetComponent<CardHolder>().CardBack = _cardBack;
        }

        _matchCode = 0;
        List<GameObject> cards = new();
        List<Sprite> cardsFront = new();

        cards.AddRange(Cards);
        cardsFront.AddRange(_cardFront);

        for (int i = 0; i != Cards.Count / 2; ++i)
        {
            int indexF = Random.Range(0, cardsFront.Count - 1);

            for (int j = 0; j != 2; ++j)
            {
                int indexC = Random.Range(0, cards.Count - 1);

                CardHolder cardsCode = cards[indexC].GetComponent<CardHolder>();

                if (cardsCode.MatchCode == -1)
                {
                    cardsCode.MatchCode = _matchCode;
                    cardsCode.CardFront = cardsFront[indexF];

                    cards.RemoveAt(indexC);
                }
            }

            cardsFront.RemoveAt(indexF);
            ++_matchCode;
        }
    }

    int _currentScene = 0;

    public void GameSceneChange()
    {
        if (_currentScene == 0)
        {
            SceneManager.LoadScene(1);
            _currentScene = 1;
        }
        else
        {
            SceneManager.LoadScene(0);
            _currentScene = 0;
        }
    }

    public void Gallery()
    {

    }

    //----------------------------------------------------

    CardHolder _firstCard;
    CardHolder _secondCard;
    Sprite _cardsSprite;

    public bool CanReveal
    {
        get { return _secondCard == null; }
    }

    public void CardRevealed(CardHolder card)
    {
        if (_firstCard == null)
            _firstCard = card;
        else
        {
            _secondCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if (_firstCard.MatchCode == _secondCard.MatchCode)
        {
            _cardsSprite = _firstCard.CardFront;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            _firstCard.Unreveal();
            _secondCard.Unreveal();

        }

        _firstCard = null;
        _secondCard = null;
    }
}
