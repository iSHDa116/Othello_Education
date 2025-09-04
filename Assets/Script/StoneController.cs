using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum StoneInfo{Empty, Black, White}

public class StoneController : MonoBehaviour
{
    public GameObject stonePrefab;
    BoardManager bm;
    public static StoneController instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("StoneControllerが複数あります!!");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void FirstStone()
    {
        PutStone(StoneInfo.White, 3, 3);
        PutStone(StoneInfo.Black, 3, 4);
        PutStone(StoneInfo.Black, 4, 3);
        PutStone(StoneInfo.White, 4, 4);
    }

    public void PutStone(StoneInfo stoneColor, int x, int y)
    {
        // 駒を生成する
        GameObject stone = Instantiate(stonePrefab, new Vector3(x, y, 1), Quaternion.identity);
        // 駒の色を白か黒にする
        stone.GetComponent<SpriteRenderer>().color = (stoneColor == StoneInfo.Black) ? Color.black : Color.white;
        BoardManager.boardInfo[x, y] = stoneColor;

        stone.gameObject.name = $"{stoneColor}Stone_{x}_{y}";
        GameObject tile = GameObject.Find($"Tile_{x}_{y}");
        stone.transform.parent = tile.transform;

        if (BoardManager.boardInfo != null)
            Debug.Log($"boardInfo[{x}, {y}]に{stone.gameObject.name}を追加しました。");

        GameManager.isBlack = !GameManager.isBlack;
        //Debug.Log($"交代。今は{GameManager.isBlack}です");
    }

    void FlipStone(Vector2Int targetPos)
    {
        if (BoardManager.boardInfo[targetPos.x, targetPos.y] == StoneInfo.Empty || bm.IsOutBoard(targetPos.x, targetPos.y))
            return;

        StoneInfo myStone = (GameManager.isBlack) ? StoneInfo.Black : StoneInfo.White;
        StoneInfo opponentStone = (GameManager.isBlack) ? StoneInfo.White : StoneInfo.Black;

        Vector2Int[] directions = {
            new Vector2Int(0,1),
            new Vector2Int(0, -1),
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0),
            new Vector2Int(-1, 1),
            new Vector2Int(1, 1),
            new Vector2Int(-1, -1),
            new Vector2Int(1,-1),
        };

        foreach (Vector2Int dir in directions)
        {
            int x = targetPos.x;
            int y = targetPos.y;


            int newX = x + dir.x;
            int newY = y + dir.y;
            List<Vector2Int> stoneToFlip = new List<Vector2Int>();

            while (bm.IsOutBoard(newX, newY) && BoardManager.boardInfo[newX, newY] == opponentStone)
            {
                stoneToFlip.Add(new Vector2Int(newX, newY));
                newX += dir.x;
                newY += dir.y;
            }

            if (bm.IsOutBoard(newX, newY) || BoardManager.boardInfo[newX, newY] == myStone)
            {
                for (int i = 0; i < stoneToFlip.Count; i++)
                {
                    int stoneX = stoneToFlip[i].x;
                    int stoneY = stoneToFlip[i].y;

                    BoardManager.boardInfo[stoneX, stoneY] = myStone;

                    Transform tile = BoardManager.tileInfo[stoneX, stoneY].transform;

                    for (int j = 0; j < tile.childCount; j++)
                    {
                        Transform child = tile.GetChild(j);

                        if (child.CompareTag("Stone"))
                        {
                            child.GetComponent<SpriteRenderer>().color = (GameManager.isBlack) ? Color.black : Color.white;
                            break;
                        }
                    }
                }
            }
        }
        
    }
}
