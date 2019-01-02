using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardController : MonoBehaviour
{

    public GameObject[] GetGameButtons() {
        // Debug.Log("Getting game buttons");
        GameObject[] gameButtons = new GameObject[9];
        int count = 0;
        foreach(Transform gameButton in transform) {
            // Debug.Log("game button:", gameButton.gameObject);
            gameButtons[count] = gameButton.gameObject;
            count = count + 1;
        }
        return gameButtons;
    }

}
