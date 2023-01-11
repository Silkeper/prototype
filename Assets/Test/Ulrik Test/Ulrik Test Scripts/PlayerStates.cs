using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    // Start is called before the first frame update
    public int PlayerState;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerStatesVoid();
    }

    private void PlayerStatesVoid()
    {
        switch(PlayerState)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
            
    }
}
