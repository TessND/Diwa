using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int CardsCount;
    [SerializeField] public bool Match;
    public float SecondClick;

    [SerializeField] int _matchCode;
    [SerializeField] Sprite _cardBack;
    [SerializeField] List<Sprite> _cardFront;
    [SerializeField] private List<GameObject> _cards;


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

        foreach (var card in _cards)
        {
            card.GetComponent<CardHolder>().CardBack = _cardBack;
        }

        CardsManagment();
    }

    private void Update()
    {
        if (CardsCount == 2 && Time.time > SecondClick + 1)
        {
            foreach (var card in _cards)
            {
                card.GetComponent<CardHolder>().SpriteCard.sprite = _cardBack;
                card.GetComponent<CardHolder>().SpriteCard.raycastTarget = true;
                NullCardsCount();
            }

        }
    }

    public void NullCardsCount()
    {
        CardsCount = 0;
    }

    private void CardsManagment()
    {
        _matchCode = 0;
        List<GameObject> cards = new();
        List<Sprite> cardsFront = new();

        cards.AddRange(_cards);
        cardsFront.AddRange(_cardFront);

        for (int i = 0; i != _cards.Count / 2; ++i)
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
}
