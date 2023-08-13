using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.Equals(gameObject.transform.position))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            ++GameManager.Instance.CardsCount;
        }

        if (transform.GetChild(0).gameObject.activeSelf && GameManager.Instance.CardsCount == 0)
            transform.GetChild(0).gameObject.SetActive(false);

    }
}
