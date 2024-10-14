using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask tankMask;                        
    public ParticleSystem explosionParticles;         
    public AudioSource explosionAudio;                
    public float maxDamage = 100f;                    
    public float explosionForce = 1000f;              
    public float maxLifeTime = 2f;                    
    public float explosionRadius = 5f;                


    private void Start ()
    {
        Destroy (gameObject, maxLifeTime);
    }


    private void OnTriggerEnter (Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere (transform.position, explosionRadius, tankMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody> ();

            if (!targetRigidbody)
                continue;

            targetRigidbody.AddExplosionForce (explosionForce, transform.position, explosionRadius);

            TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth> ();

            if (!targetHealth)
                continue;

            float damage = CalculateDamage (targetRigidbody.position);

            targetHealth.TakeDamage (damage);
        }

        explosionParticles.transform.parent = null;

        explosionParticles.Play();

        explosionAudio.Play();

        Destroy (explosionParticles.gameObject, explosionParticles.duration);

        Destroy (gameObject);
    }


    private float CalculateDamage (Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;

        float explosionDistance = explosionToTarget.magnitude;

        float relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;

        float damage = relativeDistance * maxDamage;

        damage = Mathf.Max (0f, damage);

        return damage;
    }
}