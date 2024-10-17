using System.Collections;
using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public int id;
    public LayerMask tankMask;
    public ParticleSystem explosionParticles;
    public AudioSource explosionAudio;
    public float maxDamage = 100f;
    public float maxLifeTime = 2f;
    public float explosionRadius = 5f;


    private void Start()
    {
        maxDamage = GameManager.Ins.configTank.maxDamage;
        maxLifeTime = GameManager.Ins.configTank.maxLifeTime;
        StartCoroutine(DestroyBullet());
    }
    private IEnumerator DestroyBullet()
    {

        yield return new WaitForSeconds(maxLifeTime);

        explosionParticles.transform.parent = null;

        explosionParticles.Play();

        explosionAudio.Play();

        Destroy(explosionParticles.gameObject, explosionParticles.main.duration);

        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("MainHouse") && id == 1)
        {

            TankHealth targetHealth = other.GetComponent<TankHealth>();

            targetHealth.TakeDamage(maxDamage);
            explosionParticles.transform.localScale = Vector3.one * 3f;
        }

        if (other.CompareTag("Wooden"))
        {
            TankHealth targetHealth = other.GetComponent<TankHealth>();
            targetHealth.TakeDamage(maxDamage); 
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, tankMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();
            TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

            if (!targetHealth)
                continue;

            float damage = CalculateDamage(targetRigidbody.position);

            targetHealth.TakeDamage(damage);
        }

        explosionParticles.transform.parent = null;

        explosionParticles.Play();

        explosionAudio.Play();

        Destroy(explosionParticles.gameObject, explosionParticles.main.duration);

        Destroy(gameObject);
    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;

        float explosionDistance = explosionToTarget.magnitude;

        float relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;

        float damage = relativeDistance * maxDamage;

        damage = Mathf.Max(0f, damage);

        return damage;
    }
}