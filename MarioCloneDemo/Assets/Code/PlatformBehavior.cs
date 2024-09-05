using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    private Vector2 startPosition;
    private bool isMovingRight = true;

    [SerializeField] bool canMove;
    [SerializeField] bool canRotate;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float offset;
    [SerializeField] private float rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        Debug.Log(startPosition);
    }

    // Update is called once per frame
    void Update()
    {
        LoopPlatform();
    }

    private void LoopPlatform()
    {
        if(canMove)
        {
            MovePlatform();
        }
        if(canRotate)
        {
            RotatePlatform();
        }
        
    }

    private void RotatePlatform()
    {
        transform.Rotate(0,0, rotationSpeed * Time.deltaTime);
    }

    private void MovePlatform()
    {
        if(transform.position.x >= startPosition.x + offset)
        {
            isMovingRight = false;
        }

        if(transform.position.x <= startPosition.x - offset)
        {
            isMovingRight = true;
        }


        if(isMovingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
    }
}
