using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void QuitGame(){
        Application.Quit();
    }
    public void StartGame(){
        FindObjectOfType<ScenePersist>().RestScenePersist();
        SceneManager.LoadScene(2);
        
    }
     
}
