using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController playerController;
    [Header("Controller Settings, must use CharacterController")]
    [Tooltip("Speed at which the player walks, adjust if needed")]
    public float speed = 7.0f;
    [Tooltip("Speed at which the player sprints, adjust if needed")]
    public float sprintSpeed = 10.0f;
    [Tooltip("Jump height for when the player jumps, adjust if needed")]
    public float jumpHeight = 9.0f;
    
    [Tooltip("Automatic set to the normal gravity value, adjust if needed or for testing, gravity is needed for CharacterController")]
    public float gravity = -9.81f; //normal gravity value for jumping
    private float playerVelocity; //vertical velocity for jumping

    private GameObject _pauseMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        _pauseMenu = GameObject.Find("UI").transform.Find("PauseMenu").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            MainManager.Instance.PauseGame(_pauseMenu);
        }
        
        float appliedSpeed = speed;
        // basic movement using .move()
        // grabs input from the user
        // jumping is added 
       
        // CharacterController has a built in function to check if the player is currently on the ground based on their last movement, no collision check needed
        if (playerController.isGrounded)
        {
            //Debug.Log("player is grounded");
            playerVelocity = -0.3f;
            //ensure the player is grounded
            // isGrounded may flag false without this
        }

        // Checks if the player is holding the sprint key and, if so, changes the applied speed to our sprint speed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            appliedSpeed = sprintSpeed;
        }
        
        float h = Input.GetAxisRaw("Horizontal"); 
        float v = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.right * h + transform.forward * v; // makes sure to follow the movement of the camera, does not work properly without it
        move *= appliedSpeed; //multiplying the move vector by the player speed/sprint speed
        move = AdjustVelocityToSlope(move);
        
        if (Input.GetButtonDown("Jump") && playerController.isGrounded)
        {
            playerVelocity += Mathf.Sqrt(jumpHeight * -2.0f * gravity); // physics formula for jumping
        }

        playerVelocity += gravity * Time.deltaTime; // always apply gravity to the player
        move.y += playerVelocity; // apply the player's velocity to the movement vector;
        playerController.Move(move * Time.deltaTime); // .move is called, noted that .move should be called only once
        //Debug.Log(playerController.isGrounded); 
        
    }

    private Vector3 AdjustVelocityToSlope(Vector3 velocity)
    {
        Ray ray = new(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 2.0f))
        {
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            var adjustedVelocity = slopeRotation * velocity;
            
            if (adjustedVelocity.y < 0) return adjustedVelocity;
        }
        
        return velocity;
    }
}