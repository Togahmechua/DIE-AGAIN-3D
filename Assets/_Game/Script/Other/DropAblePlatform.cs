using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAblePlatform : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Transform model;
    [SerializeField] private Transform movePos;

    private bool isAbleToMove;

    private void Update()
    {
        if (isAbleToMove)
        {
            model.position = Vector3.MoveTowards(model.position, movePos.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = Cache.GetPlayerController(other);
        if (player != null )
        {
            player.transform.parent = model.transform;
            isAbleToMove = true;
        }
    }
}
