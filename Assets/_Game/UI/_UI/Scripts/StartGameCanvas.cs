using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameCanvas : UICanvas
{
    [SerializeField] private Button startBtn;

    private void Start()
    {
        startBtn.onClick.AddListener(() =>
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.click);
            UIManager.Ins.CloseUI<StartGameCanvas>();
            UIManager.Ins.OpenUI<ChooseLevelCanvas>();
        });
    }
}
