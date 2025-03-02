﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int id;
    public ELevel eLevl;
    public PlayerController playerController;

    private void Update()
    {
        if (!LevelManager.Ins.isWin)
            return;

        if (eLevl == LevelManager.Ins.mapSO.mapList[LevelManager.Ins.curMap].eLevel &&
               !LevelManager.Ins.mapSO.mapList[LevelManager.Ins.curMap].isWon)
        {
            LevelManager.Ins.mapSO.mapList[LevelManager.Ins.curMap].isWon = true;
            SaveWinState(LevelManager.Ins.curMap);
            Debug.Log("Map " + LevelManager.Ins.curMap + " is won.");
            LevelManager.Ins.curMap++;
        }

        SetCurMap();
    }

    private void SetCurMap()
    {
        PlayerPrefs.SetInt("CurrentMap", LevelManager.Ins.curMap);
        PlayerPrefs.Save();
    }

    private void SaveWinState(int mapIndex)
    {
        string key = "MapWin_" + mapIndex;
        PlayerPrefs.SetInt(key, 1); // Lưu lại trạng thái thắng của map
        PlayerPrefs.Save();
        LevelManager.Ins.mapSO.LoadWinStates();
    }
}
