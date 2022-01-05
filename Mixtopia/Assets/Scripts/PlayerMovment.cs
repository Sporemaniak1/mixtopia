using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    
    [SerializeField]private float speed;
    [SerializeField]private float SpeedInAir;
    private Rigidbody2D RB;
    private Animator anim;
    private Collider2D coll;
    private bool OnGround;
    public bool TouchingWall;
    public bool FacingRight;
    public bool CanJump;
    public bool WallSliding;
    public Transform GroundCheck;
    public Transform WallCheck;
    public float groundCheckRadius;
    public float WallSlideSpeed;
    public float wallCheckDistance;
    public LayerMask whatIsGround;
    public int fires = 0;
    

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();    
    }

    private void Update()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
     
        if(!WallSliding){

            RB.velocity = new Vector2(HorizontalInput * speed, RB.velocity.y);
            if (HorizontalInput > 0f)
            {
                transform.localScale = new Vector3(6, 6, 1);
               
                // transform.localPosition= new Vector2(RB.position.x-0.25f , RB.position.y);
                FacingRight = true;
            }
            else if (HorizontalInput < 0f)
            {
                FacingRight = false;
                transform.localScale = new Vector3(-6, 6, 1);
                

            }
        }
     
        if (WallSliding) 
        {
            if (RB.velocity.y < -WallSlideSpeed) 
            { RB.velocity = new Vector2(RB.velocity.x, -WallSlideSpeed); }
        }
        
       

        if (Input.GetKey(KeyCode.Space)){ Jump(); }
        CheckIfCanJump();
        CheckIfCanWallSlide();
        CheckSurroundings();
        anim.SetBool("Run", HorizontalInput != 0);
        anim.SetBool("OnGround", OnGround);

        anim.SetBool("WallSlide", WallSliding);
    }


    private void CheckSurroundings() 
    {
        OnGround = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
        if (FacingRight)
        { TouchingWall = Physics2D.Raycast(WallCheck.position, transform.right, wallCheckDistance, whatIsGround); }
        else 
        {
            //WallCheck.position = new Vector2(-WallCheck.position.x, WallCheck.position.y,);
            TouchingWall = Physics2D.Raycast(new Vector2(WallCheck.position.x, WallCheck.position.y), transform.right, -wallCheckDistance, whatIsGround);
        }
        
    }

    private void Jump()
    {
        if (CanJump)
        {
            RB.velocity = new Vector2(RB.velocity.x, speed * 2.5f);
            
        }
    }


 
    //stare wykrywanie ziemi
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") 
        {
            OnGround = true;
        }
    }
    */

    private void CheckIfCanJump() 
    {
        if (OnGround||WallSliding) { CanJump = true; }
        else { CanJump = false; }
    }

    private void CheckIfCanWallSlide() 
    {
    if(TouchingWall && !OnGround && RB.velocity.y < 0) 
        {
            WallSliding = true;
        }
        else { WallSliding = false; }
    
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, groundCheckRadius);


        Gizmos.DrawLine(WallCheck.position, new Vector3(WallCheck.position.x+wallCheckDistance,WallCheck.position.y,WallCheck.position.z));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectable")
        {
            Destroy(collision.gameObject);
            fires += 1;
        }
    }
}
