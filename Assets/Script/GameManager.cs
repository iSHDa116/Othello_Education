using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isBlack = true;
    [SerializeField] UIManager ui;
    [SerializeField] BoardManager bm;

    public static GameManager instance;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        ui.TurnText(isBlack);
    }

    public void TurnChange()
    {
        isBlack = !isBlack;
        ui.TurnText(isBlack);

        Debug.Log($"交代。今は{ui.isPlayerText.text}です");
        bm.HighLightTiles();

    }
    public void FlipStone()
    {

    }
}