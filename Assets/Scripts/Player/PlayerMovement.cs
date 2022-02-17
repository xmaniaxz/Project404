using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float WalkSpeed = 5;
    public float RunningSpeed = 20;
    public float JumpHeight = 7;
    private Rigidbody rb;

    [Header("Audio")]
    public AudioSource Audio;

    [Header("GroundCheck")]
    public GameObject GroundCheck;
    [Range(0f, 1f)] public float CheckRadius = 0.5f;
    public LayerMask FloorMask;

    public void Start() => rb = GetComponent<Rigidbody>();

    public void Update()
    {
        if (CameraMovement.inMenu || !CameraMovement.inFocus) return;
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            if (Audio.isPlaying && !IsGrounded())
            {
                Audio.Pause();
                return;
            }
                if (!Audio.isPlaying)
                Audio.Play();
         
        }
        
        else if (Audio.isPlaying)
            Audio.Pause();

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity += transform.up * JumpHeight;
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(GroundCheck.transform.position, CheckRadius, FloorMask);
    }

    public void FixedUpdate()
    {
        if (!CameraMovement.inFocus || CameraMovement.inMenu) return;

        Vector3 forwardMovement = Input.GetAxis("Vertical") * GetCurrentSpeed() * transform.forward;
        Vector3 sidewardMovement = Input.GetAxis("Horizontal") * WalkSpeed * transform.right;
        //Movement for running and falling
        rb.velocity = forwardMovement + sidewardMovement + new Vector3(0, rb.velocity.y + -9.4f * Time.deltaTime, 0);
    }

    /// <summary> Get the speed the player is currently moving in</summary>
    /// <returns>float (speed)</returns>
    private float GetCurrentSpeed()
    {
        return Input.GetKey(KeyCode.LeftShift) ? RunningSpeed : WalkSpeed;
    }
}