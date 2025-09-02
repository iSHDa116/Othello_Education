using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StoneInfo{Empty, Black, White}

public class StoneController : MonoBehaviour
{
    [SerializeField] GameObject stonePrefab;
    BoardManager bm;

    // Start is called before the first frame update
    void Start()
    {

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
        stone.transform.parent = this.transform;

        if (BoardManager.boardInfo != null)
            Debug.Log($"boardInfo[{x}, {y}]に{stone.gameObject.name}を追加しました。");

        GameManager.isBlack = !GameManager.isBlack;
        //Debug.Log($"交代。今は{GameManager.isBlack}です");
    }
}
