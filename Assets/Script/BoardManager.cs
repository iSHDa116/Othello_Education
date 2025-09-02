using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static int width = 8, height = 8; //盤面のマス目の数。widthは横のマス目の数、heightは縦のマス目の数。
    public static StoneInfo[,] boardInfo = new StoneInfo[width, height]; //盤面の情報を格納する2次元配列。各マスの状態を表す。
    [SerializeField] GameObject tilePrefab; //タイルのプレハブ
    [SerializeField] StoneController stoneCtrler;
    void Start()
    {
        MakeBoard(); //盤面を作成
        stoneCtrler.FirstStone();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MakeBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //マス目の生成
                GameObject tile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                tile.transform.SetParent(transform); //タイルをBoardManagerの子オブジェクトに設定
                tile.name = "Tile_" + x + "_" + y; //タイルの名前
                boardInfo[x, y] = StoneInfo.Empty; //初期状態は0（空
                Debug.Log($"boardInfo[{x}, {y}]を{boardInfo[x, y]}にしました");
            }
        }
    }




}
