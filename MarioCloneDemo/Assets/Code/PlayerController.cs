using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpSpeed;

    [SerializeField] private Vector2 direction;

    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private bool isOnGround;

    [SerializeField] private int maxJumps = 2;
    private int jumpCount = 0;
    private Transform originalParent;

    private Vector3 lastPlatformPosition;
    private bool isOnPlatform = false;
    private Transform platformTransform;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        originalParent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        CheckGroundStatus();
        UpdatePlatformMovement();
    }

    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * moveInput * runSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            rb2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            jumpCount++;
            isOnPlatform = false;
        }
    }

    private void CheckGroundStatus()
    {
        if (isOnGround)
        {
            jumpCount = 0;
        }
    }

        private void UpdatePlatformMovement()
    {
        if (isOnPlatform && platformTransform != null)
        {
            Vector3 platformMovement = platformTransform.position - lastPlatformPosition;

            transform.position += platformMovement;

            lastPlatformPosition = platformTransform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            jumpCount = 0;
        }

        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            isOnGround = true;
            jumpCount = 0;
            platformTransform = collision.transform;
            lastPlatformPosition = platformTransform.position;
            isOnPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }

        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            isOnGround = false;
            isOnPlatform = false;
            platformTransform = null;
        }
    }
}
