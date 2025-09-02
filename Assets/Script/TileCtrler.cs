using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCtrler : MonoBehaviour
{
    int x, y;
    StoneController stoneCtrler;
    // Start is called before the first frame update
    void Start()
    {
        x = Mathf.RoundToInt(this.transform.position.x);
        y = Mathf.RoundToInt(this.transform.position.y);
    }
    void OnMouseDown()
    {
        if(stoneCtrler == null) Debug.LogError("stoneCtrler is null");
        if (GameManager.isBlack)
        {
            stoneCtrler.PutStone(StoneInfo.Black, x, y);
        }
        else if (!GameManager.isBlack)
        {
            stoneCtrler.PutStone(StoneInfo.White, x, y);
        }
    }
}
