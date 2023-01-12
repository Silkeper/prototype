using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Boatmovementsmooth : MonoBehaviour
{
    Inputs input;


    private CharacterController characterController;
    [SerializeField] private float currentmoveSpeed = 2;
    private float accelaration = 2;
    private float maxMoveSpeed = 5;


    private float moveSmoothTime = 1;

    private Vector2 direction;
    private Vector2 currentDirection = Vector2.zero;
    private Vector2 currentDirectionVelocity = Vector2.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        input = GetComponent<Inputs>();
        

    }


    void Update()
    {
        UpdateMovement();
        RememberDirection();
        Acceleration();
        Flip();
    }

    private void UpdateMovement()
    {

        Vector3 velocity = ((transform.up * direction.y) +
                        (transform.right * direction.x)) * currentmoveSpeed;

        characterController.Move(velocity * Time.deltaTime);
    }

    private void RememberDirection()
    {
        if (input.MoveVector != Vector2.zero)
        {
            direction = input.MoveVector;
        }

        /*if (input.MoveVector.x != 0)
        {
            direction.x = input.MoveVector.x;
        }
        
        if (input.MoveVector.y != 0)
        {
            direction.y = input.MoveVector.y;
        }*/
    }

    private void Acceleration()
    {
        if (input.MoveVector != Vector2.zero && currentmoveSpeed <= maxMoveSpeed) //do more stuff-
        {
            currentmoveSpeed += accelaration * Time.deltaTime;
        }
        
        else if (currentmoveSpeed > 0)
        {
            currentmoveSpeed -= accelaration * Time.deltaTime;
        } 
        
        else
        {
            currentmoveSpeed = 0;
        }
    }

    private void Flip()
    {
        if (input.MoveVector.x > 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        
        if (input.MoveVector.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        
        if (input.MoveVector.y > 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        
        if (input.MoveVector.y < 0)
        {
            gameObject.transform.localScale = new Vector3(1, -1, 1);
        }
    }

    /*private void UpdateMovementSmooth()
    {
       

        currentDirection = Vector2.SmoothDamp(currentDirection, input.MoveVector,
           ref currentDirectionVelocity, moveSmoothTime);


        Vector3 velocity = ((transform.up * currentDirection.y) +
                            (transform.right * currentDirection.x)) *
                             moveSpeed + Vector3.up * velocity;
        characterController.Move(velocity * Time.deltaTime);

    }*/
}
