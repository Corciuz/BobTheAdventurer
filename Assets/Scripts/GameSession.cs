using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    public int playerLives=3;
    public int Score=0;
    [SerializeField] TextMeshProUGUI LivesText;
    [SerializeField] TextMeshProUGUI ScoreText;
    void Awake()
    {
        int numGameSession=FindObjectsOfType<GameSession>().Length;
       
   
        if(numGameSession>1){
            
            Destroy(gameObject);
            
        }else{
            
            DontDestroyOnLoad(gameObject);
        }
    }
     void Start() {
       
    LivesText.text=playerLives.ToString();    
    ScoreText.text=Score.ToString(); 
    }
    void Update(){
 int currentSceneIndex=SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex=currentSceneIndex+1;
        if(nextSceneIndex==SceneManager.sceneCountInBuildSettings){
            Destroy(gameObject);
            
        }
    }

    public void ProcessPlayerDeath(){
        if(playerLives>1){
         TakeLife();
        }else{
            ResetGameSession();
        }
    }
    public void AddToScore(int PointsToAdd){
        Score+=PointsToAdd;
        ScoreText.text=Score.ToString();
    }
    public void AddTolife(int LifeToAdd){
        playerLives+=LifeToAdd;
     LivesText.text=playerLives.ToString();
    }
     void TakeLife()
    {
        playerLives--;
        int currentScene=SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
        LivesText.text=playerLives.ToString(); 
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().RestScenePersist();
        SceneManager.LoadScene(1);
        Destroy(gameObject);
    }
}
