
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //create variable reference
    // e.g. public <type> <name>:
    public Rigidbody2D body;
    //[SerializeField] to create variable change within Unity
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;

    //Awake only runs when script is called
    private void Awake(){
        //Fine RigidBody2d component of object where script is, assign properties to the variable
        body = GetComponent<Rigidbody2D>();
    }

    // Update checks every frame, good for movement
    private void Update(){
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        if(Input.GetAxis("Jump"));
            body.velocity = new Vector2(body.velocity.x, Input.GetAxis("Jump") * jumpHeight);
    }
}
