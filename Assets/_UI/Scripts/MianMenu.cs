using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MianMenu : UICanvas
{
    public Button buttonPlayGame;
    private void Start() {
        buttonPlayGame.onClick.AddListener(PlayButton);
    }
    public void PlayButton()
    {
        GameManager.ChangeState(GameState.GamePlay);
        UIManager.Ins.OpenUI<GamePlay>();
        Close(0);
    }
}
