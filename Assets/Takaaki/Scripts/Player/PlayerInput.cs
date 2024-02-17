using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //ÉvÉåÉCÉÑÅ[Ç…ä÷Ç∑ÇÈì¸óÕÇä«óù

    private PlayerMover playerMover;

    public void SetUp() 
    {
        playerMover = StageManager.instance.playerMover;
    }


    public void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerMover.MoveCheck(0,-1);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerMover.MoveCheck(-1, 0);

        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerMover.MoveCheck(0, 1);

        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerMover.MoveCheck(1, 0);

        }
    }
}
