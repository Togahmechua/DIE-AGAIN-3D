using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private Level preLevel;
    private Vector3 offSet;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            FollowPlayer();
        }
    }

    public void GetPlayer()
    {
        if (LevelManager.Ins.level != null)
        {
            player = LevelManager.Ins.level.playerController;
            offSet = transform.position - player.transform.position;
        }
        else
        {
            SetNullParent();
        }
    }

    public void SetNullParent()
    {
        //Debug.Log("Null");
        transform.parent = null;
        transform.position = startPos;
    }

    private void FollowPlayer()
    {
        transform.parent = player.transform;
    }
}
