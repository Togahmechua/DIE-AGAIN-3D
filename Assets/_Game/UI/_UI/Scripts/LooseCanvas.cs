using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LooseCanvas : UICanvas
{
    [Header("---Other Button---")]
    [SerializeField] private Button retryBtn;
    [SerializeField] private Button menuBtn;

    [Header("---Music Button---")]
    [SerializeField] private Button soundBtn;
    [SerializeField] private Sprite[] spr;

    private bool isClick;

    private void OnEnable()
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.loose);
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    private void Start()
    {
        menuBtn.onClick.AddListener(() =>
        {
            LevelManager.Ins.DespawnMap();
            UIManager.Ins.CloseUI<MainCanvas>();
            UIManager.Ins.CloseUI<LooseCanvas>();
            UIManager.Ins.OpenUI<ChooseLevelCanvas>();
        });

        retryBtn.onClick.AddListener(() =>
        {
            UIManager.Ins.CloseUI<LooseCanvas>();
            //SceneManager.LoadScene(0);
            //LevelManager.Ins.level.playerController.OnInit();
            LevelManager.Ins.LoadMapByID(LevelManager.Ins.curMapID);
        });

        soundBtn.onClick.AddListener(() =>
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.click);
            isClick = !isClick;
            soundBtn.image.sprite = spr[isClick ? 1 : 0];

            if (isClick)
            {
                AudioManager.Ins.TurnOff();
            }
            else
            {
                AudioManager.Ins.TurnOn();
            }
        });
    }
}
