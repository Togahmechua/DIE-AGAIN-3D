using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBtn : MonoBehaviour
{
    public int id;
    public Image img;
    public Sprite[] spr;
    public Text txt;
    public Button btn;

    [SerializeField] private Animator anim;

    private void Start()
    {
        btn.onClick.AddListener(LoadLevel);
    }

    private void LoadLevel()
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.click);
        UIManager.Ins.OpenUI<MainCanvas>();
        LevelManager.Ins.LoadMapByID(id);
        GameManager.Ins.cam.GetPlayer();
        
        UIManager.Ins.CloseUI<ChooseLevelCanvas>();
    }

    public void PlayAnim()
    {
        anim.Play(CacheString.TAG_LVBTN);
    }
}
