using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    public float speed = 4.5f;
    public float jumpHeight = 1.8f;
    public float gravity = -12f;

    private Vector3 velocity;
    private bool isGrounded;

    public bool canMove = true; // NEU

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (controller == null || animator == null || !canMove) return;

        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = camRight * inputX + camForward * inputZ;
        if (move.magnitude > 1f) move.Normalize();

        controller.Move(move * speed * Time.deltaTime);

        if (move.magnitude > 0.1f)
        {
            Quaternion toRot = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRot, 10f * Time.deltaTime);
        }

        animator.SetBool("isWalking", move.magnitude > 0.1f);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        bool isJumping = !isGrounded && velocity.y > 0.1f;
        bool isFalling = !isGrounded && velocity.y < -0.1f;

        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isFalling", isFalling);
    }
}
