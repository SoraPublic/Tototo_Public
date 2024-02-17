using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInputManager : MonoBehaviour
{
    //Stage‚ÌƒV[ƒ“‚Ì“ü—Í‚ğŠÇ—

    private PlayerInput playerInput;

    public void SetUp() 
    {
        playerInput = StageManager.instance.playerInput;
    }

    private void Update()
    {
        if(StageManager.instance.state == StageManager.State.Battle)
        {
            playerInput.InputKey();
        }
    }
}
