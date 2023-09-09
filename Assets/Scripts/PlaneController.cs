using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaneController : MonoBehaviour
{
    ////Following is for Movement

    public float moveSpeed = 5f;      
    private Rigidbody2D rb;               
    private Vector2 movementInput;
    private Vector2 velocity;

    ////Following is for Controls

    public FixedJoystick fixedJoystick;
    private bool _controlsEnabled = false;


    ////Following is for Power ups

    public CircleCollider2D cc;
    public List<PowerUp> powerUps;
    private PowerUpType currentPowerUp;
    public GameObject speedBoost;
    public GameObject invincibility;


    ////Following is for UI

    public GameObject resultPanel;

    public TMP_Text coinText;

    public float coinScore = 0f;

    public TMP_Text timeText;

    public float startTime = 0f;

    ////Following is for Misc

    public MissleSpawner missileSpawner;

    public AudioManager audioManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        cc = GetComponent<CircleCollider2D>();

        coinScore = 0f;

        startTime = 0f;

        DisableControls();
    }

    private void Update()
    {
        if (_controlsEnabled)
        {            
            float horizontalInput = fixedJoystick.Horizontal;
            float verticalInput = fixedJoystick.Vertical;            
            movementInput = new Vector2(horizontalInput, verticalInput);

            //Following is for Time
            startTime = Time.timeSinceLevelLoad;
            
            int minutes = Mathf.FloorToInt(startTime / 60);
            int seconds = Mathf.FloorToInt(startTime % 60);
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            coinText.text = coinScore.ToString("F2");
        }
        
    }

    private void FixedUpdate()
    {
        if (_controlsEnabled)
        {            
            velocity = movementInput.normalized * moveSpeed;
            
            rb.velocity = velocity;
            
            if (movementInput.magnitude > 0.1f)
            {
                float angle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;

                rb.rotation = angle;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {

            if (currentPowerUp == PowerUpType.None)
            {           
                PowerUp randomPowerUp = powerUps[Random.Range(0, powerUps.Count)];
                ActivatePowerUp(randomPowerUp.type, randomPowerUp.duration);
                Destroy(other.gameObject); // Destroy the power-up object
                audioManager.PlayPowerUpSound();
            }
        }        
        else if (other.CompareTag("Coin"))
        {
            coinScore += 1f;

            audioManager.PlayCoinSound();

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Missile"))
        {
            audioManager.PlayExplosionSound();

            missileSpawner.DisableThis();
            
            DisableControls();            

            resultPanel.SetActive(true);            
        }

    }

    private void ActivatePowerUp(PowerUpType type, float duration)
    {
        // Handle the activation of the selected power-up based on its type
        switch (type)
        {
            case PowerUpType.SpeedBoost:
                StartCoroutine(SpeedBoost(duration));
                break;

            case PowerUpType.Invincibility:
                StartCoroutine(Invincibility(duration));
                break;
        }
    }

    private IEnumerator SpeedBoost(float duration)
    {
        speedBoost.SetActive(true);        
        moveSpeed *= 2; 
        currentPowerUp = PowerUpType.SpeedBoost;
        yield return new WaitForSeconds(duration);
        moveSpeed /= 2; 
        speedBoost.SetActive(false);
        currentPowerUp = PowerUpType.None; 
    }

    private IEnumerator Invincibility(float duration)
    {
        cc.enabled = false;
        invincibility.SetActive(true);
        currentPowerUp = PowerUpType.Invincibility;
        yield return new WaitForSeconds(duration);
        invincibility.SetActive(false);
        cc.enabled = true;
        currentPowerUp = PowerUpType.None; 
    }


    public void EnableControls()
    {
        _controlsEnabled = true;

        cc.enabled = true;

        Time.timeScale = 1;        
    }

    public void DisableControls()
    {
        _controlsEnabled = false;

        cc.enabled = false;

        Time.timeScale = 0;

        rb.velocity = Vector2.zero;
    }


}

public enum PowerUpType
{
    None,
    SpeedBoost,
    Invincibility
}

[System.Serializable]
public class PowerUp
{
    public PowerUpType type;
    public float duration;
}
