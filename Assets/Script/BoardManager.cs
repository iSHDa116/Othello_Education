using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{
    //盤面の情報
    public static int width = 8, height = 8; //盤面のマス目の数。widthは横のマス目の数、heightは縦のマス目の数。
    public static StoneInfo[,] boardInfo = new StoneInfo[width, height]; //盤面の情報を格納する2次元配列。各マスの状態を表す。
    public static Transform[,] tileInfo = new Transform[width, height];
    [SerializeField] GameObject tilePrefab; //タイルのプレハブ

    //インスタンス化
    public static BoardManager instance;
    [SerializeField] StoneManager stoneCtrler;
    [SerializeField] TileManager tm;
    [SerializeField] GameManager gm;

    //判定の向き
    Vector2Int[] directions = 
        {
            new Vector2Int(1, 0), // →右
            new Vector2Int(-1, 0), // ←左
            new Vector2Int(0, 1), // ↑上
            new Vector2Int(0, -1), // ↓下
            new Vector2Int(1, 1), //　↗︎右上
            new Vector2Int(-1, 1), //　↖︎左上
            new Vector2Int(1, -1), //　↘︎右下
            new Vector2Int(-1, -1), //　↙︎左斜め下
        };
    
    //シングルトンの作成
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        MakeBoard(); //盤面を作成
        stoneCtrler.FirstStone(); //最初の駒をセット
        Invoke("HighLightTiles",0.1f); //0.1秒後にハイライトを生成する。Invoke()をつけることで、originalColorを正しく取得できる様にしています
    }
    //盤面を生成する
    void MakeBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //マス目を生成
                GameObject tile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                tile.transform.SetParent(transform); //タイルをBoardManagerの子オブジェクトに設定
                tile.name = "Tile_" + x + "_" + y; //タイルの名前

                tileInfo[x, y] = tile.transform;//tileInfoに生成したマスを登録
                boardInfo[x, y] = StoneInfo.Empty; //初期状態は0（空

                //Null処理
                if (boardInfo[x, y] == StoneInfo.Empty)
                {
                    Debug.Log($"boardInfo[{x}, {y}]を{boardInfo[x, y]}にしました");
                }
                if (tileInfo[x, y] != null)
                {
                    Debug.Log($"tileInfo[{x}, {y}]に{tileInfo[x, y].name}を追加しました");
                }
            }
        }
    }
    //盤面内にいるかの判定。trueなら盤面にいる。falseなら盤面外
    public bool IsOutBoard(int x, int y)
    {
        return (x >= 0 && x < width && y >= 0 && y < height);
    }
    //駒を置けるマスを黄色にする
    public void HighLightTiles()
    {
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < height; x++)
            {
                //マスの方法を取得
                TileCtrler tile = tileInfo[x, y].GetComponent<TileCtrler>();

                //もし指定した場所に駒を置けたら
                if (IsCanPutStone(x, y))
                {
                    //マスを黄色に変える
                    tm.TileChangeHighLight(tile);
                }
                else
                {
                    //元の色に戻す
                    tm.TileChangeOriginalColor(tile);
                }
            }
        }
    }
    //駒を置けるかの判定
    public bool IsCanPutStone(int x, int y)
    {
        //盤外だったり、すでに駒があった場合は、処理を実行しない様にする。
        if (boardInfo[x, y] != StoneInfo.Empty || !IsOutBoard(x, y))
            return false;

        // 自分の現在の駒の色を条件分岐で判定
        StoneInfo myStone = (GameManager.isBlack) ? StoneInfo.Black : StoneInfo.White;
        StoneInfo opponentStone = (!GameManager.isBlack) ? StoneInfo.Black : StoneInfo.White;

        foreach (Vector2Int dir in directions)
        {
            //dirの方向に、相手の駒が無いか1マスずつ調べる
            int newX = x + dir.x; //横方向に1マスずらす
            int newY = y + dir.y; //縦方向に1マスずらす

            //駒があるか？ないか？の判定
            bool hasOpponent = false;
            int opponents = 0;

            //盤面の中なら
            while (IsOutBoard(newX, newY))
            {
                if (boardInfo[newX, newY] == opponentStone)
                {
                    newX += dir.x; //
                    newY += dir.y; //
                    hasOpponent = true; //
                    opponents++;
                    //Debug.Log($"{dir}の方向に、ひっくり返せる駒が{opponents}マスありました");
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
                        //Debug.Log($"判定終了。{dir}の方向にひっくり返せるマスはありませんでした。");
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }
        //Debug.Log($"Tile_{x}_{y}には置けません");
        return false;
    }
    //盤面上の駒をひっくり返す
    public void FlipStone(Vector2Int pos)
    {
        if (boardInfo[pos.x, pos.y] == StoneInfo.Empty || IsOutBoard(pos.x, pos.y))
            return;

        StoneInfo myStone = (GameManager.isBlack) ? StoneInfo.Black : StoneInfo.White;
        StoneInfo opponentStone = (GameManager.isBlack) ? StoneInfo.White : StoneInfo.Black;

        foreach (Vector2Int dir in directions)
        {
            int newX = pos.x + dir.x;
            int newY = pos.y + dir.y;
            List<Vector2Int> stoneToFlip = new List<Vector2Int>();

            while (IsOutBoard(newX, newY) && boardInfo[newX, newY] == opponentStone)
            {
                stoneToFlip.Add(new Vector2Int(newX, newY));
                newX += dir.x;
                newY += dir.y;
            }

            if (IsOutBoard(newX, newY) || boardInfo[newX, newY] == myStone)
            {
                foreach (Vector2Int stoneFlip in stoneToFlip)
                {
                    int stoneX = stoneFlip.x;
                    int stoneY = stoneFlip.y;

                    boardInfo[stoneX, stoneY] = myStone;

                    Transform tile = tileInfo[stoneX, stoneY].transform;
                    Transform stone = tile.transform.Find("Stone");
                    stone.GetComponent<SpriteRenderer>().color = (GameManager.isBlack) ? Color.black : Color.white;
                }
            }
        }

    }
}
