using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Reference the physics we applied to the player 
    private Rigidbody2D body;
    private Animator anim;
    //references the grounded condition
    private bool Grounded;
    // Creates a variable for player speed. Serializedfield makes the float directly editable from within unity
    [SerializeField] private float speed;

    //calls everytime you start the game 
    private void Awake()
    {
        //checks the player object for Rigidbody2D and stores it in the body variable 
        body = GetComponent<Rigidbody2D>();
        //Reference the animator for the object 
        anim = GetComponent<Animator>();
    }
    // runs on player input 
    private void Update()
    {
        //creates and defines the variable for horizontal input 
        float horizontalInput =  Input.GetAxis("Horizontal");

        //registers horizontal input form the player 
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
        

        //flips the player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector2.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector2(-1, 1);

        //checks if the spacebar has been pressed. Input.GetKey can only have 2 values: true when the key is pressed and false when its not
        if (Input.GetKey(KeyCode.Space) && Grounded)
            //References the Jump void
            Jump();

        //sets Animator parameters
        anim.SetBool("Walk", horizontalInput != 0);
        anim.SetBool("Grounded", Grounded);
    }
    private void Jump()
    {
        //defines what happens when the space key is pressed
        //when space is pressed  this code will maintain the current velocity on the x axis and apply a velocity of the speed variable on the Y axis.
        //you can swap the speed variable with a number to tweak the jump
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        //sets the value of the trigger
        anim.SetTrigger("Jump");
        //tells the program that when the player jumps it is not on the ground
        Grounded = false;
    }
    //checks the object the player is colliding with
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            Grounded = true;
    }
}
