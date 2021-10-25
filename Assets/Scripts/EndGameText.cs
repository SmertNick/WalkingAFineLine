using UnityEngine;
using UnityEngine.UI;

public class EndGameText : MonoBehaviour
{
    [SerializeField] private string loseMessage = "Потрачено";
    [SerializeField] private string winMessage = "You Won";

    private Text endText;
    private string[] messages;

    private void Start()
    {
        messages = new string[2] { loseMessage, winMessage };
        endText = GetComponent<Text>();
    }

    public void ShowText(int id)
    {
        endText.text = messages[id];
    }
}
