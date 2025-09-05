using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    //駒を置けるマスを光らせる
    public void TileChangeHighLight(TileCtrler tileObj)
    {
        // 子オブジェクト"SUrface"を探して、見つかったら変数childに代入する
        Transform child = tileObj.transform.Find("Surface");
        //マスを黄色にする
        child.GetComponent<SpriteRenderer>().color = Color.yellow;
    }
    //駒を置けないマスは元の色に戻す
    public void TileChangeOriginalColor(TileCtrler tileObj)
    {
        // 子オブジェクト"SUrface"を探して、見つかったら変数childに代入する
        Transform child = tileObj.transform.Find("Surface");
        //マスを元の色にする
        child.GetComponent<SpriteRenderer>().color = tileObj.originalColor;
    }
}
