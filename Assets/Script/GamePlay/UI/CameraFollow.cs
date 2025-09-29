using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;       // Nhân vật (Player) cần follow
    public float smoothSpeed = 0.125f; // Độ mượt khi camera di chuyển
    public Vector3 offset;         // Khoảng cách lệch giữa camera và nhân vật

    void LateUpdate()
    {
        if (target == null) return;

        // Lấy vị trí mong muốn theo nhân vật
        Vector3 desiredPosition = target.position + offset;

        // Giữ nguyên trục Z của camera (không thay đổi)
        desiredPosition.z = transform.position.z;

        // Nội suy mượt
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Gán vị trí mới cho camera
        transform.position = smoothedPosition;
    }
}
