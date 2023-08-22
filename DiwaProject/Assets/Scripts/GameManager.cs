using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int CardsCount;
    public int Tries;
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

            CurrentScene = SceneManager.GetActiveScene().buildIndex;

            if (CurrentScene != 0)
            {
                SceneManager.LoadScene(0);
                CurrentScene = 0;
            }
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        CardsCount = 0;
    }

    public void NullCardsCount()
    {
        CardsCount = 0;
    }

    public void ShuffleCards()
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

    public int CurrentScene;

    public IEnumerator GameSceneChange()
    {
        if (CurrentScene == 0)
        {
            SceneManager.LoadScene(1);
            CurrentScene = 1;

            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            SceneManager.LoadScene(0);
            CurrentScene = 0;

            Cards.RemoveRange(0,10);

            yield return new WaitForSeconds(0.1f);
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
            CardsManagment();
        
    }

    private void CardsManagment()
    {
        GameObject cardsCanvas = GameObject.FindGameObjectWithTag("Card");

        for (int i = 0; i != cardsCanvas.transform.childCount; ++i)
            cardsCanvas.transform.GetChild(i).GetComponent<CardHolder>().CardsStart();
        
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

            --Tries;
            _firstCard.Unreveal();
            _secondCard.Unreveal();

        }

        _firstCard = null;
        _secondCard = null;
    }
}
