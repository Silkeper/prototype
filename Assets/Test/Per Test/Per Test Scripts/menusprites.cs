using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class menusprites : MonoBehaviour
{
    [SerializeField] FishStatsScripts fishstats;

    //banned for being naugthy
    /*
    [SerializeField] private Image tooth;
    [SerializeField] private Image Leg;
    [SerializeField] private Image Blue;
    [SerializeField] private Image Milk;
    [SerializeField] private Image DadFesk;
    [SerializeField] private Image Narwhal;
    [SerializeField] private Image fish;
    */

    [SerializeField] private GameObject toothDescription;
    [SerializeField] private GameObject legDescription;
    [SerializeField] private GameObject blueDescription;
    [SerializeField] private GameObject milkDescription;
    [SerializeField] private GameObject dadDescription;
    [SerializeField] private GameObject narwhalDescription;
    [SerializeField] private GameObject fishDescription;
    [SerializeField] private GameObject scaredfishDescription;
    [SerializeField] private GameObject flyingfishDescription;


    public GameObject undiscoveredToothfish;
    public GameObject undiscoveredLegFish;
    public GameObject undiscoveredBlueFish;
    public GameObject undiscoveredMilkFish;
    public GameObject undiscoveredDad;
    public GameObject undiscoveredNarwhal;
    public GameObject undiscoveredfish;
    public GameObject scaredundiscoveredfish;
    public GameObject undiscoveredflyingfish;

    /*
    [SerializeField] private Sprite toothFishSprite;
    [SerializeField] private Sprite LegFishSprite;
    [SerializeField] private Sprite BlueFishSprite;
    [SerializeField] private Sprite MilkFishSprite;
    [SerializeField] private Sprite DadFishSprite;
    [SerializeField] private Sprite NarwhalSprite;
    [SerializeField] private Sprite FishSprite;*/

    void Start()
    {

        //fishstats = gameObject.GetComponent<FishStatsScripts>();

        toothDescription.SetActive(false);
        fishDescription.SetActive(false);
        blueDescription.SetActive(false);
        legDescription.SetActive(false);
        dadDescription.SetActive(false);
        milkDescription.SetActive(false);
        narwhalDescription.SetActive(false);
        scaredfishDescription.SetActive(false);
        flyingfishDescription.SetActive(false);
    }


    void Update()
    {
        if (fishstats.Fesk > 0)
        {
            undiscoveredToothfish.SetActive(false);

            toothDescription.SetActive(true);
        }

        if (fishstats.fish > 0)
        {

            undiscoveredfish.SetActive(false);

            fishDescription.SetActive(true);
        }

        if (fishstats.BlueFesk > 0)
        {
            undiscoveredBlueFish.SetActive(false);

            blueDescription.SetActive(true);
        }
        
        if (fishstats.LegFesk > 0)
        {
            undiscoveredLegFish.SetActive(false);

            legDescription.SetActive(true);
        }
        
        if (fishstats.Dad > 0)
        {
            undiscoveredDad.SetActive(false);

            dadDescription.SetActive(true);
        }

        if (fishstats.MelkFesk > 0)
        {
            undiscoveredMilkFish.SetActive(false);

            milkDescription.SetActive(true);
        }

        if (fishstats.FourWingedNarwhal > 0)
        {
            undiscoveredNarwhal.SetActive(false);

            narwhalDescription.SetActive(true);
        }

        if (fishstats.scared > 0)
        {
            scaredundiscoveredfish.SetActive(false);

            scaredfishDescription.SetActive(true);
        }

        if (fishstats.Seagull > 0)
        {
            undiscoveredflyingfish.SetActive(false);

            flyingfishDescription.SetActive(true);
        }
    }
}
