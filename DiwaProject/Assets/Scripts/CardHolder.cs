using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHolder : MonoBehaviour
{
    [SerializeField] float _timeFlip;

    public Image SpriteCard;

    public int MatchCode = -1;
    public Sprite CardBack;
    public Sprite CardFront;

    private void Start()
    {
        SpriteCard = GetComponent<Image>();
    }

    public void CardsStart()
    {
        GameManager.Instance.Cards.Add(this.gameObject);
        SpriteCard.raycastTarget = false;

        if (GameManager.Instance.Cards.Count == 10)
            GameManager.Instance.ShuffleCards();
    }

    public void Unreveal()
    {
        SpriteCard.sprite = CardBack;
        SpriteCard.raycastTarget = true;
    }

    public void CardHolderFlip()
    {
        if (SpriteCard.sprite != CardFront && GameManager.Instance.CanReveal)
        {
            SpriteCard.sprite = CardFront;
            SpriteCard.raycastTarget = false;

            GameManager.Instance.CardRevealed(this);
        }
    }
}
