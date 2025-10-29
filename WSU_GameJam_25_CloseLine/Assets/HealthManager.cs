using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int startHealth = 100; 
    [SerializeField] private Image healthFill;

    public Canvas ui;

    private int currentHealth;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentHealth = Mathf.Clamp(startHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0) return;

        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (amount <= 0) return;

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthFill != null)
        {
            healthFill.fillAmount = (float)currentHealth / maxHealth;
        }
        else
        {
            
            Debug.LogWarning("HealthManager: healthFill Image is not assigned in the Inspector.");
        }
    }

    private void Die()
    {
        Debug.Log("Player died");
         UnityEngine.Debug.Log("Players got touched by NPC. Game should end now.");
         Camera camera = Camera.main;
            if (camera != null)
            {
                camera.backgroundColor = Color.black;
                ui.gameObject.SetActive(true);
                AudioSource sfxplayer = camera.GetComponent<AudioSource>();
                sfxplayer.clip = (AudioClip)Resources.Load("Loss");
                sfxplayer.Play();
            }
       Time.timeScale = 0;
    }
}
