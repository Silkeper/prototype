using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;

public class PlayerFishing : MonoBehaviour
{
    private Inputs input;
    private FishStatsScripts fishStats;
    [SerializeField] private GameObject bobber;
    private Rigidbody2D bobberRb;
    private SpriteRenderer bobberRenderer;

    private float currentBobberSpeed;
    private float MaxBobberSpeed = 10;
    private float bobberSpeedDecay = 13;

    private float bobberRecallSpeed = 5;
    private bool isBobberPositionReset;

    private int bobberState;

    private float fishTimeWindow = 1;
    private float currentFishTimeWindow;
    private float currentTimeBeforeFish;

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
    }

    // Update is called once per frame
    void Update()
    {
        BobberStates();
        PullBobberWhenOutOfRange();
        
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
                bobberRenderer.sprite = bobberSprite;
                isBobberPositionReset = false;
                bobber.SetActive(false);
                if(input.ActionValue)
                {
                    //print("Casting Bobber");
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
                    currentTimeBeforeFish = Random.Range(1f,6f);
                    whatFish = Random.Range(1,8);
                    print(currentTimeBeforeFish);
                    currentFishTimeWindow = 0;
                    hasFishBit = false;
                }
                break;
            case 2:
                //bobber exists
                if(currentFishTimeWindow > 0)
                {
                    // bobberRenderer.sprite = bobberSprite;
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
                }/*
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
                }*/
                bobberRenderer.sprite = bobberSprite;
                currentBobberSpeed = 0;
                bobberRb.velocity = Vector2.zero;
                if(input.ActionValue)
                {
                    if (currentFishTimeWindow > 0)
                    {
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
                //The bobber stays to find fish
                break;
            case 3:
                var step = bobberRecallSpeed * Time.fixedDeltaTime;
                bobber.transform.position = Vector3.MoveTowards(bobber.transform.position, transform.position, step);
                //The bobber is recalled
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
    private void OnTriggerEnter2D(Collider2D other)
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
