using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : UICanvas
{
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Button jumpBtn;
    [SerializeField] private Button changeMoveType;

    private void OnEnable()
    {
        UIManager.Ins.mainCanvas = this;
    }

    private void Start()
    {
        jumpBtn.onClick.AddListener(() =>
        {
            if (LevelManager.Ins.level.playerController != null)
            {
                LevelManager.Ins.level.playerController.Jump();
            }
        });

        changeMoveType.onClick.AddListener(() =>
        {
            if (LevelManager.Ins.level.playerController != null)
            {
                joystick.ResetInput();
                LevelManager.Ins.level.playerController.ToggleMoveType();
            }
        });

        //Tắt Navigation để tránh kích hoạt khi ấn Space
        changeMoveType.navigation = new Navigation { mode = Navigation.Mode.None };

        UpdateMoveUI(LevelManager.Ins.level.playerController.moveType);
    }

    public void UpdateMoveUI(EMoveType moveType)
    {
        joystick.gameObject.SetActive(moveType == EMoveType.Joystick);
        jumpBtn.gameObject.SetActive(moveType == EMoveType.Joystick);
    }

    public FixedJoystick GetJoystick()
    {
        return joystick;
    }
}
