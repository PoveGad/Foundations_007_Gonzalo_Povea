using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTwoManager : MonoBehaviour
{

    [SerializeField] private GameObject[] Targets;
    
    [SerializeField] private GameObject ResetLevel;

    [SerializeField] private TMP_Text Text;
    

    private int Score;
    private int TargetHitted;
    
    
    private void Start()
    {
        
        StartGame();
    }

    public void StartGame()
    {
        ResetLevel.SetActive(false);
        Score = 0;
        TargetHitted = 0;
        ActualizateScore();
        SpawnTargets();
    }

    private void SpawnTargets()
    {
        foreach (GameObject target in Targets)
        {
            target.SetActive(true);
        }
    }

    public void BaseTargetHit()
    {
       Score++;
       ActualizateScore();
       TargetHitted++;
       if (TargetHitted == Targets.Length)
       {
           FinishedGame();
       }

    }

    private void ActualizateScore()
    {
        Text.text = Score.ToString();
    }

    public void CenterTargetHit()
    {
        Score += 3;
        ActualizateScore();
        TargetHitted++;
        if (TargetHitted == Targets.Length)
        {
            FinishedGame();
        }

    }

    private void FinishedGame()
    {
        
        ResetLevel.SetActive(true);
    }
}
