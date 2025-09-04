using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static int width = 8, height = 8; //盤面のマス目の数。widthは横のマス目の数、heightは縦のマス目の数。
    public static StoneInfo[,] boardInfo = new StoneInfo[width, height]; //盤面の情報を格納する2次元配列。各マスの状態を表す。
    public static Transform[,] tileInfo = new Transform[width, height];
    [SerializeField] GameObject tilePrefab; //タイルのプレハブ
    [SerializeField] StoneController stoneCtrler;
    [SerializeField] GameManager gm;
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
    public bool IsCanFlipStone(Vector2Int pos)
    {
        //盤外だったり、すでに駒があった場合は、処理を実行しない様にする。
        if (boardInfo[pos.x, pos.y] != StoneInfo.Empty || IsOutBoard(pos.x, pos.y))
            return false;

        // 自分の現在の駒の色を条件分岐で判定
        StoneInfo myStone = (GameManager.isBlack) ? StoneInfo.Black : StoneInfo.White;
        StoneInfo opponentStone = (GameManager.isBlack) ? StoneInfo.White : StoneInfo.Black;

        // 
        Vector2Int[] directions = {
            new Vector2Int(0,1), //
            new Vector2Int(0, -1), //
            new Vector2Int(1, 0), //
            new Vector2Int(-1, 0), //
            new Vector2Int(-1, 1), //
            new Vector2Int(1, 1), //
            new Vector2Int(-1, -1), //
            new Vector2Int(1,-1), //
        };

        foreach (Vector2Int dir in directions)
        {
            int newX = pos.x + dir.x; //
            int newY = pos.y + dir.y; //
            //駒があるか？ないか？の判定
            bool hasOpponent = false;
            
            //盤面の中なら
            while (IsOutBoard(newX, newY))
            {
                if (boardInfo[newX, newY] == opponentStone)
                {
                    newX += dir.x; //
                    newY += dir.y; //
                    hasOpponent = true; //
                }
                else if (boardInfo[newX, newY] == myStone) //
                {
                    //
                    if (hasOpponent)
                    {
                        return true;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        return false;
    }
    //盤面内にいるかの判定。trueなら盤面にいる。falseなら盤面外
    public bool IsOutBoard(int x, int y)
    {
        return (x >= 0 && x < width && y >= 0 && y < height);
    }
}
