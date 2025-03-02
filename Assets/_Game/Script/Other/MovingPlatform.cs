using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform model;
    [SerializeField] private Transform[] movePos;
    [SerializeField] private float speed = 2f;

    private int currentTargetIndex = 0;
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (movePos.Length < 2) return;

        model.position = Vector3.MoveTowards(model.position, movePos[currentTargetIndex].position, speed * Time.deltaTime);

        if (Vector3.Distance(model.position, movePos[currentTargetIndex].position) < 0.1f)
        {
            currentTargetIndex = (currentTargetIndex + 1) % movePos.Length; // Chuyển sang điểm tiếp theo
        }
    }
}
