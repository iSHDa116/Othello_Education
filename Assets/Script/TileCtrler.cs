using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileCtrler : MonoBehaviour
{
    public int x, y;
    public Color originalColor;
    Transform surface;
    // Start is called before the first frame update
    void Start()
    {
        x = Mathf.RoundToInt(this.transform.position.x);
        y = Mathf.RoundToInt(this.transform.position.y);
        surface = transform.Find("Surface");
        Debug.Log(surface.GetComponent<SpriteRenderer>().color);
        originalColor = surface.GetComponent<SpriteRenderer>().color;
    }
    //このオブジェクトがクリックされた時
    void OnMouseDown()
    {
        Debug.Log(this.gameObject.name + "：" + BoardManager.boardInfo[x, y]);
        // もし,その場所に何も置かれていなかったら
        if (BoardManager.boardInfo[x, y] == StoneInfo.Empty)
        {
            // もし、黒の番なら
            if (GameManager.isBlack && BoardManager.instance.IsCanPutStone(x, y))
            {
                //StoneControllerから、PutStone()メソッドを呼び出して、駒を置く
                StoneController.instance.PutStone(StoneInfo.Black, x, y);
                BoardManager.instance.FlipStone(new Vector2Int(x, y));
                //ターンチェンジ
                GameManager.instance.TurnChange();
            }
            //白の番だったら
            else if (!GameManager.isBlack && BoardManager.instance.IsCanPutStone(x, y))
            {
                //StoneControllerから、PutStone()メソッドを呼び出して、駒を置く
                StoneController.instance.PutStone(StoneInfo.White, x, y);
                BoardManager.instance.FlipStone(new Vector2Int(x, y));
                //ターンチェンジ
                GameManager.instance.TurnChange();
            }
        }
        //空なじゃなかったら
        else
        {
            //ログに表示
            Debug.LogWarning("すでに駒が置かれています。");
        }
    }
}
