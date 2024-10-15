using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankShootingEnemy : MonoBehaviour
{
    public Rigidbody shell_Prefab;
    public Transform fireTransform;
    public Transform playerTransform;
    public Slider aimSlider;
    public float launchForce;
    public float currentLaunchForce;



    public void Fire()
    {
        // Bước 1: Tính khoảng cách đến player
        float distance = Vector3.Distance(fireTransform.position, playerTransform.position);

        // Bước 2: Tính lực bắn dựa trên khoảng cách
        float launchForce = CalculateLaunchForce(distance);

        // Bước 3: Bắn đạn
        Rigidbody shellInstance = Instantiate(shell_Prefab, fireTransform.position, fireTransform.rotation) as Rigidbody;
        shellInstance.velocity = launchForce * fireTransform.forward;
    }

    // Hàm tính toán lực bắn dựa trên khoảng cách và trọng lực
    private float CalculateLaunchForce(float distance)
    {
        // Giả sử góc bắn (launch angle) là 45 độ để đạt được khoảng cách tối đa
        float launchAngle = 80f * Mathf.Deg2Rad; // Chuyển đổi từ độ sang radian

        // Tính vận tốc ban đầu cần thiết dựa trên công thức:
        // v = sqrt(d * g / sin(2 * theta))
        float launchForce = Mathf.Sqrt(distance * Mathf.Abs(-9.81f) / Mathf.Sin(2 * launchAngle));

        // Giới hạn lực bắn trong khoảng từ minLaunchForce đến maxLaunchForce
        return Mathf.Clamp(launchForce, 10, 30);
    }
}
