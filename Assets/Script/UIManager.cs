using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text isPlayerText;

    public void TurnText(bool isBlack)
    {
        isPlayerText.text = (isBlack) ? "黒の番" : "白の番";
        isPlayerText.color = (isBlack) ? Color.black : Color.white;
        isPlayerText.GetComponent<Outline>().effectColor = (isBlack) ? Color.white : Color.black;

        //Debug.Log(isPlayerText.text);
    }
}