using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool flip = false;

    public void FlipCard()
    {
        flip = !flip;
    }
    
}
