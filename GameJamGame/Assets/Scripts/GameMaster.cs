using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    public PlayerController player1;
    public PlayerController player2;

    public Text winnerText;
    public Color32 player1Color;
    public Color32 player2Color;

    void Start()
    {
        if (instance == null)
        instance = this;
    }

    public void TimeRanOut(int playerID)
    {
        Debug.Log("Calling timeranout");

        winnerText.text = "Player " + playerID.ToString() + " wins!";

        switch (playerID)
        {
            case 1:
                winnerText.color = player1Color;
                break;

            case 2:
                winnerText.color = player2Color;
                break;
        }
    }
}
