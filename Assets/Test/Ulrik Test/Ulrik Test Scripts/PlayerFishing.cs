using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PlayerFishing : MonoBehaviour
{
    private Inputs input;
    private FishStatsScripts fishStats;
    [SerializeField] private GameObject bobber;
    [SerializeField] private SpriteRenderer utropsTegn;
    private Rigidbody2D bobberRb;
    private SpriteRenderer bobberRenderer;

    [SerializeField] private Transform linePoint;
    [SerializeField] private Transform bobberLinePoint;
    private LineRenderer lineRenderer;

    private float currentBobberSpeed;
    private float MaxBobberSpeed = 10;
    private float bobberSpeedDecay = 13;

    private float bobberRecallSpeed = 5;
    private bool isBobberPositionReset;

    private int bobberState;

    private float fishTimeWindow = 1;
    private float currentFishTimeWindow;
    private float currentTimeBeforeFish;

    private bool bobberMovesRight;

    private bool hasFishBit;
    [SerializeField] private Vector2 fishTimeRandom;

    [SerializeField] private int FishAmount;

    private bool isInRange;
    [SerializeField] private LayerMask isInRangeLayer;

    [SerializeField] private Sprite bobberSprite;
    [SerializeField] private Sprite feskSprite;
    [SerializeField] private Sprite blueFeskSprite;
    [SerializeField] private Sprite legFeskSprite;
    [SerializeField] private Sprite DadSprite;
    [SerializeField] private Sprite MelkFeskSprite;
    [SerializeField] private Sprite fishSprite;
    [SerializeField] private Sprite fourWingedNarWhalSprite;
    private int whatFish;
    void Start()
    {
        input = GetComponent<Inputs>();
        fishStats = GetComponent<FishStatsScripts>();
        bobberRb = bobber.GetComponent<Rigidbody2D>();
        bobberRenderer = bobber.GetComponent<SpriteRenderer>();
        lineRenderer = GetComponent<LineRenderer>();
        utropsTegn.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        BobberStates();
        PullBobberWhenOutOfRange();
        if(bobberState != 0)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, linePoint.position);
            lineRenderer.SetPosition(1, bobberLinePoint.position);
        }
        else
        {
            lineRenderer.enabled = false;
        }
        
    }
    private void FixedUpdate()
    {
        
    }

    private void BobberStates()
    {
        switch(bobberState)
        {

            case 0:
                utropsTegn.enabled = false;
                //The bobber is inactive
                bobberRenderer.sprite = bobberSprite;
                isBobberPositionReset = false;
                bobber.SetActive(false);
                if(input.ActionValue)
                {
                    //print("Casting Bobber");
                    bobberState = 1;
                    if(input.MoveVector.x >= 0)
                    {
                        bobberMovesRight = true;
                    }
                    if (input.MoveVector.x < 0)
                    {
                        bobberMovesRight = false;
                    }
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
                if(bobberMovesRight)
                {
                    bobberRb.velocity = Vector2.right * currentBobberSpeed;
                }
                else
                {
                    bobberRb.velocity = Vector2.left * currentBobberSpeed;
                }
                
                currentBobberSpeed -= bobberSpeedDecay * Time.deltaTime;
                if(currentBobberSpeed <= 0)
                {
                    bobberState = 4;
                }
                break;
            case 2:
                //bobber exists
                
                if (currentFishTimeWindow < 0)
                {
                    bobberRenderer.sprite = bobberSprite;
                    /*
                    if (whatFish == 1)
                    {
                        bobberRenderer.sprite = feskSprite;
                    }
                    if (whatFish == 2)
                    {
                        bobberRenderer.sprite = blueFeskSprite;
                    }
                    if (whatFish == 3)
                    {
                        bobberRenderer.sprite = legFeskSprite;
                    }
                    if (whatFish == 4)
                    {
                        bobberRenderer.sprite = DadSprite;
                    }
                    if (whatFish == 5)
                    {
                        bobberRenderer.sprite = MelkFeskSprite;
                    }
                    if (whatFish == 6)
                    {
                        bobberRenderer.sprite = fishSprite;
                    }
                    if (whatFish == 7)
                    {
                        bobberRenderer.sprite = fourWingedNarWhalSprite;
                    }
                    */
                }
                else
                {
                    if (whatFish == 1)
                    {
                        bobberRenderer.sprite = feskSprite;
                    }
                    if (whatFish == 2)
                    {
                        bobberRenderer.sprite = blueFeskSprite;
                    }
                    if (whatFish == 3)
                    {
                        bobberRenderer.sprite = legFeskSprite;
                    }
                    if (whatFish == 4)
                    {
                        bobberRenderer.sprite = DadSprite;
                    }
                    if (whatFish == 5)
                    {
                        bobberRenderer.sprite = MelkFeskSprite;
                    }
                    if (whatFish == 6)
                    {
                        bobberRenderer.sprite = fishSprite;
                    }
                    if (whatFish == 7)
                    {
                        bobberRenderer.sprite = fourWingedNarWhalSprite;
                    }
                }
               // bobberRenderer.sprite = bobberSprite;
                currentBobberSpeed = 0;
                bobberRb.velocity = Vector2.zero;
                if(input.ActionValue)
                {
                    if (currentFishTimeWindow > 0)
                    {
                        hasFishBit = false;
                        FishAmount += 1;
                        currentFishTimeWindow = 0;
                        print("YOU GOT FISH!");
                        WhatFish();
                    }
                    bobberState = 3;
                }
                if(currentFishTimeWindow <= 0 && !hasFishBit)
                {
                    currentTimeBeforeFish -= Time.deltaTime;
                }

                if(currentFishTimeWindow >= 0)
                {
                    utropsTegn.enabled = true;
                }
                else
                {
                    utropsTegn.enabled = false;
                }
                
                if(currentTimeBeforeFish <= 0)
                {
                currentFishTimeWindow = fishTimeWindow;
                    currentTimeBeforeFish = 1;
                    print("FISH!");
                    hasFishBit = true;
                }
                if(!isInRange)
                {
                    var step1 = bobberRecallSpeed * Time.fixedDeltaTime;
                    bobber.transform.position = Vector3.MoveTowards(bobber.transform.position, transform.position, step1);
                }
                currentFishTimeWindow -= Time.deltaTime;
                if(currentFishTimeWindow <= 0 && hasFishBit)
                {
                    bobberState = 4;
                }
                //The bobber stays to find fish
                break;
            case 3:
                utropsTegn.enabled = false;
                var step = bobberRecallSpeed * Time.fixedDeltaTime;
                bobber.transform.position = Vector3.MoveTowards(bobber.transform.position, transform.position, step);
                //The bobber is recalled
                break;
            case 4:
                
                currentTimeBeforeFish = Random.Range(1f, 6f);
                whatFish = Random.Range(1, 8);
                print(currentTimeBeforeFish);
                currentFishTimeWindow = -20;
                hasFishBit = false;
                bobberState = 2;
                break;
        }
    }

    private void PullBobberWhenOutOfRange()
    {
        Vector2 origin1 = bobber.transform.position;
        Vector2 direction = Vector2.up;
        isInRange = Physics2D.Raycast(origin1, direction, 0.1f, isInRangeLayer);
        Debug.DrawRay(origin1, direction * 0.1f, Color.red);

    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("bobber") && bobberState == 3)
        {
            bobberState = 0;
        }
    }
    private void WhatFish()
    {
        if(whatFish == 1)
        {
            fishStats.Fesk += 1;
        }
        if (whatFish == 2)
        {
            fishStats.BlueFesk += 1;
        }
        if (whatFish == 3)
        {
            fishStats.LegFesk += 1;
        }
        if (whatFish == 4)
        {
            fishStats.Dad += 1;
        }
        if (whatFish == 5)
        {
            fishStats.MelkFesk += 1;
        }
        if (whatFish == 6)
        {
            fishStats.fish += 1;
        }
        if (whatFish == 7)
        {
            fishStats.FourWingedNarwhal += 1;
        }

    }
}
