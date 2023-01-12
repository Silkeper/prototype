using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishMenu : MonoBehaviour
{
    Inputs inputs;
    private bool isMenuOpen = false;

    public GameObject canvasObject;
    

    void Start()
    {
        inputs = GetComponent<Inputs>();
        canvasObject.SetActive(false);
    }

    

    void Update()
    {

        if (inputs.menufishValue)
        {

            if (isMenuOpen == false)
            {
                canvasObject.SetActive(true);

                isMenuOpen = true;
            }

            else if (isMenuOpen == true)
            {
                canvasObject.SetActive(false);

                isMenuOpen = false;
            }

        }

        
    }
}
