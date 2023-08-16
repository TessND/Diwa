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

    public void CardHolderFlip()
    {
        if (SpriteCard.sprite != CardFront && GameManager.Instance.CardsCount < 2)
        {
            ++GameManager.Instance.CardsCount;
            GameManager.Instance.SecondClick = Time.time;
            SpriteCard.sprite = CardFront;
            SpriteCard.raycastTarget = false;
        }
    }
}
