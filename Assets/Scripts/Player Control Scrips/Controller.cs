using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    //character rigidbody
    private Rigidbody2D rb;

    //attributes of player movement 
    public float speed;
    public float jumpForce;
    private float moveInput;

    //default direction of player used for Turn()  #temp
    private bool faceRight = true;

    //Used to determine if player is on the ground
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    //Used for Jump 
    private float jumpTimeCounter;
    private bool isJumping;
    public float jumpTime;
    public float fallMultiplier = 3f;

    //used for Bounce
    private bool doBounce = false;
    public float stopTime = 0.5f;
    [SerializeField]
    private float dropForce = 70f;
    [SerializeField]
    private float gravityScale = 1f;
    private bool isBouncing = false;
    private int wait=0;
    [SerializeField]
    private int taptime;

    //used for Roll Attack
    private float rollSpeed;
    private float rollTime;
    private float startRollTime = 0.3f;
    private int rollDirection;
    private float rollgravity;



    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        //slingTime = startSlingTime;
        rollTime = startRollTime;
        rollSpeed = speed * 2f;
        taptime = 0;
        rollgravity = 1f;

    }



    //FixedUpdate is called once every frame
    void FixedUpdate()
    {
        if(isGrounded == true)
        {
            taptime = 0;
        }

        if(isBouncing == false)
        {
            //moving character based on horizonal input
            moveInput = Input.GetAxisRaw("Horizontal");//right==1, left ==-1
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            //end of moving character (x axis)
        }

        //turning character based on horizonal input
        if (faceRight == false && moveInput > 0)
        {
            Turn();
        }
        else if (faceRight == true && moveInput < 0)
        {
            Turn();
        }
        //end of turning character


        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        //beginning of code for jumping
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        //end of code for jumping

        //code for bounce
        if (isGrounded == false && Input.GetKeyDown(KeyCode.DownArrow))
        {
            doBounce = true;
            taptime += 1;
        }
        //end of code for bounce

        //For Bouncing
        if (doBounce && !isBouncing)
        {
            BounceNow();
        }
        doBounce = false;
        //End of Bouncing

        //For Rolling
        if(rollDirection == 0 && Input.GetKeyDown(KeyCode.X))
        {
            if (faceRight == true)
            {
                rollDirection = 1;
            }
            else if (faceRight == false)
            {
                rollDirection = 2;
            }
        }
        else
        {
            if (rollTime <= 0)
            {
                rollDirection = 0;
                rollTime = startRollTime;
            }
            else
            {    
                rollTime -= Time.deltaTime;
                

                if (rollDirection == 1)
                {
                    StopInAir();
                    rb.velocity = Vector2.right * rollSpeed;
                    Wait(1);
                    rb.gravityScale = rollgravity;
                }
                else if(rollDirection == 2)
                {
                    StopInAir();
                    rb.velocity = Vector2.left * rollSpeed;
                    Wait(1);
                    rb.gravityScale = rollgravity;
                }
            }
        }
        //End of Rolling
    }



    //Turns character based on user input. This will update in future.#temp
    void Turn()
    {
        faceRight = !faceRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    //end of turning character based on user input. 

    //For Bouncing
    private void BounceNow()
    {
        isBouncing = true;
        StopInAir();
        StartCoroutine("DropDown");

    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.contacts[0].normal.y >= 0.5){
            CompleteBounce();
        }

        if (taptime > 1)
        {
            taptime = 0;
            rb.AddForce(Vector2.up * jumpForce * 2.3f, ForceMode2D.Impulse);
        }

        //for platform
        if (other.gameObject.name.Equals("Platform"))
        {
            this.transform.parent = other.transform;
        }
        //end platform
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        //for platform
        if (other.gameObject.name.Equals("Platform"))
        {
            this.transform.parent = null;
        }
        //end platform
    }

    private void StopInAir()
    {
        ClearForces();
        rb.gravityScale = 0;

    }

    private IEnumerator DropDown()
    {
        //descent
        yield return new WaitForSeconds(stopTime*2);
        
        rb.AddForce(Vector2.down * dropForce, ForceMode2D.Impulse);

        
    }

    private void CompleteBounce()
    {
        rb.gravityScale = gravityScale;
        isBouncing = false;
    }
    private void ClearForces()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }
    //end of functions for bouncing

    //this is a pause 
    private IEnumerator Wait(float t)
    {
        yield return new WaitForSeconds(t);
    }
    //end of the pause

}
