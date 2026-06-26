using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor2 : MonoBehaviour
{
    public SpriteRenderer playerSprite;

    public void MakeRed()
    {
        playerSprite.color = Color.red;
    }

    public void MakeWhite()
    {
        playerSprite.color = Color.white;
    }

    public void MakeOrange()
    {
        playerSprite.color = new Color(1f, 0.5f, 0f); // RGB for orange!
    }

    public void MakeBlue()
    {
        playerSprite.color = Color.blue;
    }

    public void MakeGreen()
    {
        playerSprite.color = Color.green;
    }

    public void MakeBlack()
    {
        playerSprite.color = Color.black;
    }
}
