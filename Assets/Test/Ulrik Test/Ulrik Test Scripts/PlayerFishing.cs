using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;

public class PlayerFishing : MonoBehaviour
{
    private UlrikTestInput input;
    [SerializeField] private GameObject bobber;
    private Rigidbody2D bobberRb;

    private float currentBobberSpeed;
    private float MaxBobberSpeed = 10;
    private float bobberSpeedDecay = 13;

    private float bobberRecallSpeed = 20;
    private bool isBobberPositionReset;

    private int bobberState;
    void Start()
    {
        input = GetComponent<UlrikTestInput>();
        bobberRb = bobber.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        BobberStates();
    }
    private void FixedUpdate()
    {
        
    }

    private void BobberStates()
    {
        switch(bobberState)
        {
            case 0:
                //The bobber is inactive
                isBobberPositionReset = false;
                bobber.SetActive(false);
                if(input.ActionValue)
                {
                    print("Casting Bobber");
                    bobberState = 1;
                }
                break;
            case 1:
                //The bobber is cast and moves
                bobber.SetActive(true);
                if(!isBobberPositionReset)
                {
                    bobber.SetActive(true);
                    bobber.transform.position = transform.position;
                    currentBobberSpeed = MaxBobberSpeed;
                    isBobberPositionReset = true;
                }
                bobberRb.velocity = Vector2.up * currentBobberSpeed;
                currentBobberSpeed -= bobberSpeedDecay * Time.deltaTime;
                if(currentBobberSpeed <= 0)
                {
                    bobberState = 2;
                }
                break;
            case 2:
                currentBobberSpeed = 0;
                bobberRb.velocity = Vector2.zero;
                if(input.ActionValue)
                {
                    bobberState = 3;
                }
                //The bobber stays to find fish
                break;
            case 3:
                var step = bobberRecallSpeed * Time.deltaTime;
                bobber.transform.position = Vector3.MoveTowards(bobber.transform.position, transform.position, step);
                //The bobber is recalled
                break;
        }
    }

    private void CastBobber()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("bobber") && bobberState == 3)
        {
            bobberState = 0;
        }
    }
}
