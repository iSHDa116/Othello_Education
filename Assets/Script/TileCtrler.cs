using UnityEngine;

public class TileCtrler : MonoBehaviour
{
    public int x, y;
    public Color originalColor;
    Transform surface;
    void Start()
    {
        //変数x、yに自分の座標を代入。何度もtransform~と書くのは大変なので
        x = Mathf.RoundToInt(this.transform.position.x);
        y = Mathf.RoundToInt(this.transform.position.y);

        //マスの色を取得
        surface = transform.Find("Surface");
        originalColor = surface.GetComponent<SpriteRenderer>().color;
    }

    //このオブジェクトがクリックされた時
    void OnMouseDown()
    {
        //クリックしたマスの情報をログに表示。念の為のログ
        Debug.Log($"クリックしたマス({this.gameObject.name}:{ BoardManager.boardInfo[x, y]})");
        // もし,その場所に何も置かれていなかったら
        if (BoardManager.boardInfo[x, y] == StoneInfo.Empty)
        {
            // もし、黒の番なら
            if (GameManager.isBlack && BoardManager.instance.IsCanPutStone(x, y))
            {
                //StoneManagerから、PutStone()メソッドを呼び出して、駒を置く
                StoneManager.instance.PutStone(StoneInfo.Black, x, y);
                BoardManager.instance.FlipStone(new Vector2Int(x, y));
                //ターンチェンジ
                GameManager.instance.TurnChange();
            }
            //白の番だったら
            else if (!GameManager.isBlack && BoardManager.instance.IsCanPutStone(x, y))
            {
                //StoneManagerから、PutStone()メソッドを呼び出して、駒を置く
                StoneManager.instance.PutStone(StoneInfo.White, x, y);
                BoardManager.instance.FlipStone(new Vector2Int(x, y));
                //ターンチェンジ
                GameManager.instance.TurnChange();
            }
            else
            {
                Debug.LogWarning("そのマスには置けません。");
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
