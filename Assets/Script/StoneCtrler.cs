using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneCtrler : MonoBehaviour
{
    public void FlipStone()
    {
        Color stoneColor = (GameManager.isBlack) ? Color.black : Color.white;
        this.GetComponent<SpriteRenderer>().color = stoneColor;
    }
}
