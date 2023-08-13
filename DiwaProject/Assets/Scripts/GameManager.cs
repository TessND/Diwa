using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int CardsCount;
    [SerializeField] public bool Match;

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

    void Update()
    {
        if (CardsCount == 2)
            CardsCount = 0;
    }

}
