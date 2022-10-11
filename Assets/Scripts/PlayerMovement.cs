using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
   Vector2 moveInput;
   Rigidbody2D myrigidbody;
   Animator MyAnimator;
   CapsuleCollider2D myBodycollider;
   BoxCollider2D myFeetCollider;
   [SerializeField] float runSpeed =5f;
   [SerializeField] float JumpSpeed =5f;
   [SerializeField] float climbSpeed=5f;
   [SerializeField] Vector2 DeathKick=new Vector2 (10f,-10f);
   [SerializeField] GameObject Bullet;
   [SerializeField] Transform Gun;
   float gravityScaleAtStart;
   bool HasJump;
   bool IsAlive=true;
    void Start()
    {
        myrigidbody=GetComponent<Rigidbody2D>();
        MyAnimator=GetComponent<Animator>();
        myBodycollider=GetComponent<CapsuleCollider2D>();
        myFeetCollider=GetComponent<BoxCollider2D>();
        gravityScaleAtStart=myrigidbody.gravityScale;
    }

    
    void Update()
    {
        if(!IsAlive){
          return;  
        }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
        
        
    }
    
    
    void OnFire(InputValue value){
         if(!IsAlive){
          return;  
        }
    Instantiate(Bullet,Gun.position,transform.rotation);
       
       
        }
    void OnMove(InputValue value){
         if(!IsAlive){
          return;  
        }
        moveInput=value.Get<Vector2>();
       
    }
    void OnJump(InputValue value){
         if(!IsAlive){
          return;  
        }
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            return;
        }
     if(value.isPressed){
      myrigidbody.velocity+=new Vector2(0f,JumpSpeed);
      
     }
     
    }
   
    void Run(){
        Vector2 playerVelocity=new Vector2(moveInput.x*runSpeed,myrigidbody.velocity.y);
        myrigidbody.velocity=playerVelocity;
       bool playerHasHorizontalSpeed=Mathf.Abs(myrigidbody.velocity.x)>Mathf.Epsilon;
        MyAnimator.SetBool("IsRunning",playerHasHorizontalSpeed);


    }
    void FlipSprite(){
        bool playerHasHorizontalSpeed=Mathf.Abs(myrigidbody.velocity.x)>Mathf.Epsilon;
        if(playerHasHorizontalSpeed){
     transform.localScale=new Vector2(Mathf.Sign(myrigidbody.velocity.x),1f);

        }
    }
    void ClimbLadder(){
         if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
            myrigidbody.gravityScale=gravityScaleAtStart;
            MyAnimator.SetBool("IsClimbing",false);
            return;
        }
        Vector2 climbVelocity=new Vector2(myrigidbody.velocity.x,moveInput.y*climbSpeed);
        myrigidbody.velocity=climbVelocity;
        myrigidbody.gravityScale=0f;
        bool playerHasVerticalSpeed=Mathf.Abs(myrigidbody.velocity.y)>Mathf.Epsilon;
        MyAnimator.SetBool("IsClimbing",playerHasVerticalSpeed);
    }
    void Die(){
        if(myBodycollider.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazards"))){
            IsAlive=false;
            MyAnimator.SetTrigger("Dying");
           myrigidbody.velocity=DeathKick;
           myBodycollider.enabled=false;
          myFeetCollider.enabled=false;
StartCoroutine(DeathAnimation());
          
        }
        
    }
    IEnumerator DeathAnimation(){
yield return new WaitForSecondsRealtime(1);
FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }
}
