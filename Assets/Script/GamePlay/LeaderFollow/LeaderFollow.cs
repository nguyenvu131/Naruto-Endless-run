using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderFollow : MonoBehaviour {

	public Transform target; // Player hoặc leader trước
    public float followDistance = 1.5f;
    public float moveSpeed = 8f;

    private Vector3 targetPos;

    void Update()
    {
        if (target == null) return;

        // Vị trí mục tiêu: đứng cách target 1 khoảng (followDistance)
        targetPos = target.position - target.right * followDistance;

        // Di chuyển mượt mà đến vị trí
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
    }
}
