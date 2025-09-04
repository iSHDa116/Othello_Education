using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCtrler : MonoBehaviour
{
    int x, y;
    // Start is called before the first frame update
    void Start()
    {
        x = Mathf.RoundToInt(this.transform.position.x);
        y = Mathf.RoundToInt(this.transform.position.y);
    }
    void OnMouseDown()
    {
        if (BoardManager.boardInfo[x, y] == StoneInfo.Empty)
        {
            if (GameManager.isBlack)
            {
                StoneController.instance.PutStone(StoneInfo.Black, x, y);
                
            }
            else if (!GameManager.isBlack)
            {
                StoneController.instance.PutStone(StoneInfo.White, x, y);
            }
        }
        else
        {
            Debug.LogWarning("すでに駒が置かれています。");
        }

    }
}
