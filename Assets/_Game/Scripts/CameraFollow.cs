using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;        // Transform của player
    public Vector3 offset;          // Khoảng cách từ camera đến player
    public float smoothSpeed = 0.125f;  // Tốc độ mượt khi di chuyển camera

    private bool isZoomedOut = false;  // Trạng thái zoom ra hay không
    public Vector3 zoomOutOffset;      // Khoảng cách zoom ra khi ấn nút
    public float zoomOutSmoothSpeed = 0.1f; // Tốc độ mượt khi zoom ra

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + (isZoomedOut ? zoomOutOffset : offset);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    // Gọi hàm này khi nhấn nút để thay đổi trạng thái zoom
    public void ToggleZoomOut()
    {
        isZoomedOut = !isZoomedOut;
    }
}