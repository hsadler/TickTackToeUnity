using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneModel : MonoBehaviour
{
    
    // the static reference to the singleton instance
    public static SceneModel instance { get; private set; }
    
    public int playerTurn = 1;
    public Text winText;
    public Text playerTurnText;
    public int[,] winConditionIndexes = new int[,] {
        // horizontal wins
        {0, 1, 2},
        {3, 4, 5},
        {6, 7, 8},
        // vertical wins
        {0, 3, 6},
        {1, 4, 7},
        {2, 5, 8},
        // diagonal wins
        {0, 4, 8},
        {2, 4, 6}
    };
    public GameObject gameBoard;
    public GameObject restartButton;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        // singleton pattern
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
        InitGame();
    }

    public void InitGame() {
        winText.text = "";
        SetTurnToPlayer(1);
        // init game buttons
        GameObject[] gameButtons = gameBoard.GetComponent<GameBoardController>().GetGameButtons();
        foreach (GameObject gb in gameButtons)
        {
            gb.GetComponent<GameButtonController>().Start();
        }
        // init restart button
        restartButton.SetActive(false);
    }

    public void TogglePlayerTurn() {
        if(playerTurn == 1) {
            SetTurnToPlayer(2);
        } else {
            SetTurnToPlayer(1);
        }
    }

    private void SetTurnToPlayer(int player) {
        playerTurn = player;
        playerTurnText.text = "Player " + player.ToString() + " Go!";
    }

    private bool CheckGameBoardStateWinCondition(string[] gameBoardState, string toCheck) {
        // Debug.Log("gameboard state string: " + string.Join("", gameBoardState));
        for(int i = 0; i < winConditionIndexes.GetLength(0); i++) {
            bool currentStatus = true;
            for(int j = 0; j < winConditionIndexes.GetLength(1); j++) {
                int gameBoardIndex = winConditionIndexes[i, j];
                string buttonState = gameBoardState[gameBoardIndex];
                // Debug.Log("checking button state: " + buttonState + " at game board index: " + gameBoardIndex.ToString());
                // Debug.Log("toCheck string: " + toCheck);
                if(buttonState != toCheck) {
                    currentStatus = false;
                }
            }
            if(currentStatus) {
                return true;
            }
        }
        return false;
    }

    private bool CheckGameBoardStateTieCondition(string[] gameBoardState) {
        foreach(string gbsItem in gameBoardState) {
            if(gbsItem == "None") {
                return false;
            }
        }
        return true;
    }

    public void CheckWinCondition() {
        // Debug.Log("checking win condition");
        // get game buttons from game board via game board's controller script
        GameObject[] gameButtons = gameBoard.GetComponent<GameBoardController>().GetGameButtons();
        // gather button states from their controller scripts
        string[] gameBoardState = new string[9];
        int i = 0;
        foreach (GameObject gameButton in gameButtons)
        {
            // Debug.Log("gamebutton:", gameButton);
            string buttonState = gameButton.GetComponent<GameButtonController>().buttonState; 
            // Debug.Log("button state: " + buttonState);
            gameBoardState[i] = buttonState;
            i = i + 1;
        }
        // check player 1 'X' win condition
        if (playerTurn == 1 && CheckGameBoardStateWinCondition(gameBoardState, "X")) {
            winText.text = "Player 1 Wins!";
            SetGameWonState();
        }
        // check player 2 'O' win condition
        else if (playerTurn == 2 && CheckGameBoardStateWinCondition(gameBoardState, "O")) {
            winText.text = "Player 2 Wins!";
            SetGameWonState();
        }
        // check cats game win condition
        else if(CheckGameBoardStateTieCondition(gameBoardState)) {
            winText.text = "Cats Game!";
            SetGameWonState();
        }
    }

    private void SetGameWonState() {
        restartButton.SetActive(true);
    }

}
