using System.Collections;
using UnityEngine;

public class TankShootingEnemy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] public float rotationSpeedTurret;
    [SerializeField] private GameObject turret;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float bulletSpeed;
    public AudioSource ShootTankAudio;


    public Rigidbody shell_Prefab;
    public Transform fireTransform;
    private bool canFire = true;

    private void Start()
    {
        attackSpeed = GameManager.Ins.configTank.attackSpeed;
        bulletSpeed = GameManager.Ins.configTank.bulletSpeed;
    }

    public void Turret()
    {
        Vector3 turretDirection = (player.transform.position - transform.position).normalized;

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
