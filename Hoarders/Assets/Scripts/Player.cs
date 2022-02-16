using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player_rigidbody;
    [SerializeField] private float player_speed;
    [SerializeField] private float jump_speed;
    [SerializeField] private bool canJump;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float hangTime = .2f;
    [SerializeField] private float timer_hangTime;
    [SerializeField] private float jumpBufferLength;
    [SerializeField] private float timer_JumpBuffer;

    //the Difference is that Awake is called when the OBJECT is initialized 
    private void Awake()
    {
        player_rigidbody = GetComponent<Rigidbody2D>();
        if (player_rigidbody == null) Debug.LogError("Rigidbody not attached ");
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

        if (canJump)
        {
            timer_hangTime = hangTime;
        }
        else
        {
            timer_hangTime -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            timer_JumpBuffer = jumpBufferLength;
        }
        else { timer_JumpBuffer -= Time.deltaTime; }


        if (Input.GetButtonDown("Jump") && Mathf.Abs(player_rigidbody.velocity.y) < 0.001f && timer_JumpBuffer >= 0 && timer_hangTime > 0f)
        {

            player_rigidbody.velocity = new Vector2(player_rigidbody.velocity.x, jump_speed);
            canJump = false;
            timer_JumpBuffer = 0;
        }






        if (Input.GetButtonUp("Jump") && player_rigidbody.velocity.y > 0f)
        {
            player_rigidbody.velocity = new Vector2(player_rigidbody.velocity.x, player_rigidbody.velocity.y * .5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other_object)
    {
        //Check if player can jump
        if (other_object.gameObject.tag == "Ground")
        {
            canJump = true;
            jumpCooldown = Time.time + 0.2f;
        }
        else if (Time.time < jumpCooldown)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }
}
