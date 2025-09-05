using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum StoneInfo{Empty, Black, White}

public class StoneController : MonoBehaviour
{
    public GameObject stonePrefab;
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
    }
}
