using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public float startingHealth = 100f;               // The amount of health each tank starts with.
    public Slider slider;                             // The slider to represent how much health the tank currently has.
    public Image fillImage;                           // The image component of the slider.
    public Color fullHealthColor = Color.green;       // The color the health bar will be when on full health.
    public Color zeroHealthColor = Color.red;         // The color the health bar will be when on no health.
    public GameObject m_ExplosionPrefab;                // A prefab that will be instantiated in Awake, then used whenever the tank dies.


    private ParticleSystem explosionParticles;        // The particle system the will play when the tank is destroyed.
    private float currentHealth;                      // How much health the tank currently has.
    private bool isDead;                                // Has the tank been reduced beyond zero health yet?


    private void Awake ()
    {
        explosionParticles = Instantiate (m_ExplosionPrefab).GetComponent<ParticleSystem> ();


        explosionParticles.gameObject.SetActive (false);
    }


    private void OnEnable()
    {
        currentHealth = startingHealth;
        isDead = false;

        SetHealthUI();
    }


    public void TakeDamage (float amount)
    {
        currentHealth -= amount;

        SetHealthUI ();

        if (currentHealth <= 0f && !isDead)
        {
            OnDeath();
        }
    }


    private void SetHealthUI ()
    {
        slider.value = currentHealth;

        fillImage.color = Color.Lerp (zeroHealthColor, fullHealthColor, currentHealth / startingHealth);
    }


    private void OnDeath ()
    {
        isDead = true;

        explosionParticles.transform.position = transform.position;
        explosionParticles.gameObject.SetActive(true);

        explosionParticles.Play();

        gameObject.SetActive (false);
    }
}