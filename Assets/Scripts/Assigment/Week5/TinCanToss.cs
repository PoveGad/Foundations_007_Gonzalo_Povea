using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TinCanToss : MonoBehaviour
{
    [SerializeField] private int _ballMaxCounter;
    [SerializeField] private int _MaxcanCount;
    
    [SerializeField] private GameObject _canva;
    [SerializeField] private TMP_Text _text;

    [SerializeField] private GameObject _BallsPrefab;
    [SerializeField] private GameObject CanGame;
    [SerializeField] private Transform BallsPosition;
    [SerializeField] private Transform CanPosition;

    private int _totalCan=0;
    private int _totalBallsUsed=0;
    private bool winned= false;

    private GameObject CanGameInst;
    private GameObject BallsInst;

    

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        SpawnObjets();
        SetInitialValues();
    }

    private void SetInitialValues()
    {
        _totalCan = 0;
        _totalBallsUsed = 0;
        winned = false;
        _canva.SetActive(false);
        _ballMaxCounter = _BallsPrefab.transform.childCount;
        _MaxcanCount = CanGame.transform.childCount;
    }

    private void SpawnObjets()
    {
        CanGameInst = Instantiate(CanGame, CanPosition.position, CanPosition.rotation);
        BallsInst = Instantiate(_BallsPrefab, BallsPosition.position, BallsPosition.rotation);
    }

    public void CanDetected()
    {
        _totalCan++;
        if (_totalCan == _MaxcanCount)
        {
            GameWinned();
        }
    }

    public void BallDetected()
    {
        _totalBallsUsed++;
        if (_totalBallsUsed == _ballMaxCounter)
        {
            if (!winned)
            {
                GameFail();
            }
            
        }
    }

    private void GameFail()
    {
        _canva.SetActive(true);
        _text.text = "You lose the game";
        _text.color = Color.red;
        StartCoroutine(RestartGame());
    }

    private void GameWinned()
    {
        winned = true;
        _canva.SetActive(true);
        _text.text = "You win the game!";
        _text.color = Color.green;
        StartCoroutine(RestartGame());

    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(4f);
        DeleteObjects();
        StartGame();
        
    }

    private void DeleteObjects()
    {
        if (CanGameInst)
        {
            Destroy(CanGameInst);
        }

        if (BallsInst)
        {
            Destroy(BallsInst);
        }
    }
}
