using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public float startingHealth = 100f;               // The amount of health each tank starts with.
    public Slider slider; 
    public Slider slider2D;                            // The slider to represent how much health the tank currently has.
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
        if(slider2D != null) slider2D.value = currentHealth;
    }
    private void OnDeath ()
    {
        if(!GameManager.IsState(GameState.GamePlay)) return;

        if(gameObject.CompareTag("Player")){
            GameManager.ChangeState(GameState.Lose);
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<Lose>();
        }
        if(gameObject.CompareTag("MainHouse")){
            GameManager.ChangeState(GameState.Win);
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<Win>();
        }
        isDead = true;
        explosionParticles.transform.localScale = Vector3.one * 3f;
        explosionParticles.transform.position = transform.position;
        explosionParticles.gameObject.SetActive(true);

        explosionParticles.Play();

        gameObject.SetActive (false);
    }
}