using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;       // Player cần theo dõi
    public float smoothSpeed = 5f; // Tốc độ mượt
    public Vector3 offset;         // Khoảng cách giữa Camera và Player

    private void LateUpdate()
    {
        if (target == null) return;

        // Camera chỉ follow theo trục X (endless run ngang)
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x,
                                              transform.position.y,
                                              transform.position.z);

        // Lerp cho mượt
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
    }
}
