using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtonController : MonoBehaviour
{
     
    private GameObject xSprite;
    private GameObject oSprite;

    public string buttonState;

    // Start is called before the first frame update
    public void Start()
    {
        xSprite = transform.Find("X").gameObject;
        xSprite.SetActive(false);
        oSprite = transform.Find("O").gameObject;
        oSprite.SetActive(false);
        buttonState = "None";
    }
    
    public void SetStateX() {
        buttonState = "X";
        xSprite.SetActive(true);
    }

    public void SetStateO() {
        buttonState = "O";
        oSprite.SetActive(true);
    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown()
    {
        if(buttonState == "None") {
            if(SceneModel.instance.playerTurn == 1) {
                SetStateX();
            } else {
                SetStateO();    
            }
            SceneModel.instance.CheckWinCondition();
            SceneModel.instance.TogglePlayerTurn();
        }
    }
}
