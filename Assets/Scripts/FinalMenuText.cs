using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalMenuText : MonoBehaviour
{
   
    [SerializeField] TextMeshProUGUI LivesText;
    [SerializeField] TextMeshProUGUI ScoreText;
   void Start()
    {
        int playerLives=FindObjectOfType<GameSession>().playerLives;
     int Score=FindObjectOfType<GameSession>().Score;
        Debug.Log(playerLives);
        Debug.Log(Score);
        LivesText.text=playerLives.ToString();    
    ScoreText.text=Score.ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
