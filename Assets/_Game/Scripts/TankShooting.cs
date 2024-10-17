using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class TankShooting : MonoBehaviour
{
    [SerializeField] private VariableJoystick joystickShoot;
    [SerializeField] public float rotationSpeedTurret;
    [SerializeField] private GameObject turret;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float bulletSpeed;
    public Rigidbody shell_Prefab;
    public Transform fireTransform;
    private bool canFire = true;

    public AudioSource ShootTankAudio;

    private void Start() {
        attackSpeed = GameManager.Ins.configTank.attackSpeed;
        bulletSpeed = GameManager.Ins.configTank.bulletSpeed;   
    }


    private void Update()
    {
        Turret();
    }

    private void Turret()
    {
        float horizontalInput = joystickShoot.Horizontal;
        float verticalInput = joystickShoot.Vertical;

        if (Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0)
        {
            Vector3 turretDirection = new Vector3(horizontalInput, 0, verticalInput);

            Quaternion desiredRotation = Quaternion.LookRotation(turretDirection, Vector3.up);

            turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, desiredRotation, rotationSpeedTurret * Time.deltaTime);

            if (Quaternion.Angle(turret.transform.rotation, desiredRotation) < 1f)
            {
                if (canFire)
                {
                    StartCoroutine(Fire());
                }
            }
        }
    }

    private IEnumerator Fire()
    {
        canFire = false;
        Rigidbody shellInstance = Instantiate(shell_Prefab, fireTransform.position, fireTransform.rotation) as Rigidbody;
        ShootTankAudio.Play();
        shellInstance.velocity = bulletSpeed * fireTransform.forward;
        yield return new WaitForSeconds(attackSpeed);
        canFire = true;
    }
}
