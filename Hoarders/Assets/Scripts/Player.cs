using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player_rigidbody;
    [SerializeField] private float player_speed;
    [SerializeField] private float jump_speed;
    [SerializeField] private bool canJump;
    

    //the Difference is that Awake is called when the OBJECT is initialized 
    private void Awake()
    {
        player_rigidbody = GetComponent<Rigidbody2D>();
        if (player_rigidbody == null) Debug.Log("Error --- Rigidbody not attached ");
    }
    //This is when the SCRIPT has been enabled so after an OBJECT been initialed 
    private void Start()
    {
        
        
    }
    //Related to anything physics
    private void FixedUpdate()
    {
        Movement();
     
    }

    private void Update()
    {
        Jump();
    }

    private void Movement()
    {
       float move = Input.GetAxisRaw("Horizontal");
       player_rigidbody.velocity = new Vector2(move * player_speed, player_rigidbody.velocity.y);
    }
    private void Jump()
    {
      
        if (Input.GetKey(KeyCode.Space) && Mathf.Abs(player_rigidbody.velocity.y) < 0.001f)
        {
           
            player_rigidbody.AddForce(new Vector2(0, jump_speed), ForceMode2D.Impulse);
            canJump = false;
               
        }
    }

    private void OnCollisionEnter2D(Collision2D other_object)
    {
        //Check if player can jump
      if(other_object.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }
}
