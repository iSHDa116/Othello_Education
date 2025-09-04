using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isBlack = true;
    [SerializeField] UIManager ui;

    public void TurnChange()
    {
        isBlack = !isBlack;
        ui.TurnText(isBlack);

    }
    public void FlipStone()
    {

    }
}