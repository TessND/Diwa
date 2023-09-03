using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int MatchCount;
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

    public void ShuffleCards()
    {
        MatchCount = 0;
        foreach (var card in Cards)
            card.GetComponent<CardHolder>().CardBack = _cardBack;

        _matchCode = 0;
        List<GameObject> cards = new();
        List<Sprite> cardsFront = new();

        cards.AddRange(Cards);
        cardsFront.AddRange(_cardFront);
        cardsFront.RemoveAt(14);

        for (int i = 0; i != Cards.Count / 2; ++i)
        {
            int indexCardsFront = Random.Range(0, cardsFront.Count); //Picture

            for (int j = 0; j != 2; ++j)
            {
                int indexCards = Random.Range(0, cards.Count); //Card itself

                CardHolder cardsHolder = cards[indexCards].GetComponent<CardHolder>();

                if (cardsHolder.MatchCode == -1)
                {
                    cardsHolder.SpriteCard.raycastTarget = true;
                    cardsHolder.MatchCode = _matchCode;
                    cardsHolder.CardFront = cardsFront[indexCardsFront];

                    cards.RemoveAt(indexCards);
                }
            }

            cardsFront.RemoveAt(indexCardsFront);
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

            MatchCount = 0;
            Tries = 5;

            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            SceneManager.LoadScene(0);
            CurrentScene = 0;
            RemoveTemporalSprites();

            Cards.RemoveRange(0, 10);
            Cards = new ();

            yield return new WaitForSeconds(0.1f);
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
            CardsManagment();
        else
            ManageGallery();

    }

    public List<int> IndexOpenedImages;
    private void ManageGallery()
    {
        GameObject eventSys = GameObject.FindGameObjectWithTag("Event System");

        if (IndexOpenedImages.Count != 0)
            for (int i = 0; i != IndexOpenedImages.Count; ++i)
                eventSys.GetComponent<GalleryManagment>().LoadGallery(IndexOpenedImages[i]);
        eventSys.GetComponent<GalleryManagment>().UpdateGallery();
        RemoveGainImages();
    }

    private void CardsManagment()
    {
        GameObject cardsCanvas = GameObject.FindGameObjectWithTag("Card");

        for (int i = 0; i != cardsCanvas.transform.childCount; ++i)
            cardsCanvas.transform.GetChild(i).GetComponent<CardHolder>().CardsStart();

    }



    //----------------------------------------------------

    CardHolder _firstCard;
    CardHolder _secondCard;
    public List<Sprite> CardsSprite;
    public List<Sprite> _temporalCardsSprite;

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
            _temporalCardsSprite.Add(_firstCard.CardFront);
            ++MatchCount;
            
            CheckMatchCount();
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            --Tries;
            if (Tries == 0)
                RemoveTemporalSprites();

            _firstCard.Unreveal();
            _secondCard.Unreveal();

        }

        _firstCard = null;
        _secondCard = null;
    }

    public void RemoveGainImages()
    {
        CardsSprite.RemoveRange(0, CardsSprite.Count);
        CardsSprite = new ();
    }

    private void CheckMatchCount()
    {
        if (MatchCount == 5)
            {
                CardsSprite.AddRange(_temporalCardsSprite);
                RemoveTemporalSprites();
            }
    }

    private void RemoveTemporalSprites()
    {
        _temporalCardsSprite.RemoveRange(0, _temporalCardsSprite.Count);
        _temporalCardsSprite = new ();
    }
}
