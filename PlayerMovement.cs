using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] to create variable change within Unity
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float wallPushback;
    [SerializeField] private float wallJumpHeight;

    //create variable reference
    // e.g. public <type> <name>:
    public Rigidbody2D body;

    private Animator anim;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private float wallJumpCooldown;
    private float horizontalInput;

    //Awake only runs when script is called
    private void Awake(){
        //Grab references for rigidbody and animator
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update checks every frame, good for movement
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");


        //will check if key is pressed constantly   
        //if(Input.GetAxis("Jump")>0){


        //flips player when moving left or right
        if(horizontalInput > 0.01f )
            transform.localScale = Vector3.one;
            else if(horizontalInput < -.01f)
                transform.localScale = new Vector3(-1, 1, 1);

        //will only check when key is pressed



            //Set Animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //wall jump logic
        if(wallJumpCooldown > 0.2f){

            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded()){
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
        else
            body.gravityScale = 3;

        if(Input.GetKey(KeyCode.Space))
            Jump();
        }
        else
            wallJumpCooldown += Time.deltaTime;
    }

    private void Jump(){
        if(isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
            anim.SetTrigger("jump");
        }
        else if(onWall() && !isGrounded())
        {
            if(horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 3);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * wallPushback, wallJumpHeight);
            }
            wallJumpCooldown = 0;

        }
    }


    private bool isGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    
    public bool canAttack(){    
        //tk make able to attack while moving
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
