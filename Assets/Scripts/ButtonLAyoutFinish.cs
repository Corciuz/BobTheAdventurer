using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLAyoutFinish : MonoBehaviour
{
    
   public void StartGame(){
        FindObjectOfType<ScenePersist>().RestScenePersist();
        SceneManager.LoadScene(3);
        
    }
}
