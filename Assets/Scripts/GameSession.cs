using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

    [SerializeField] int playerLives = 3;
    [SerializeField] int numHearts = 3;
    [SerializeField] int playerScore = 0;

    [SerializeField] Image[] hearts;
    [SerializeField] Sprite fullHeart;


    //[SerializeField] Text playerLivesText;
    [SerializeField] Text PlayerScoresText;


    private void Awake()
    {
        int myGameSessions = FindObjectsOfType<GameSession>().Length;
        if (myGameSessions>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        //playerLivesText.text = playerLives.ToString();
        PlayerScoresText.text = playerScore.ToString();
	}

    private void Update()
    {
        if (playerLives>numHearts)
        {
            playerLives = numHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i<playerLives)
            {
                hearts[i].sprite = fullHeart;
            }
          

            if (i<numHearts)
            {
                hearts[i].enabled = true;
            }

            if(i>=playerLives|| i >= numHearts)
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void TakeLives()
    {
        playerLives--;
        //playerLivesText.text = playerLives.ToString();
        if (playerLives < 1)
        {
            RestartGameSession();
        }       
    }

    public void AddScores(int addScores)
    {
        playerScore += addScores;
        PlayerScoresText.text = playerScore.ToString();
    }

    private void RestartGameSession()
    {
        GetComponent<Menu>().LoadMenu();
        Destroy(gameObject);
    }  
}
