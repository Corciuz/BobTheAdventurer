using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLifePickup : MonoBehaviour
{
   [SerializeField] AudioClip lifePickUpSFX;
    [SerializeField] int PointsForlifePickup=1;
    bool wasCollected=false;
    void OnTriggerEnter2D(Collider2D other) {

    if(other.tag=="Player"&&!wasCollected){
        wasCollected=true;
        FindObjectOfType<GameSession>().AddTolife(PointsForlifePickup);
        AudioSource.PlayClipAtPoint(lifePickUpSFX,Camera.main.transform.position);
        gameObject.SetActive(false);
    Destroy(gameObject);
    }
    
   }
}
