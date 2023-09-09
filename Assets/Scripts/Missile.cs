using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 5f;      
    public float rotationSpeed = 10f; 
    public float lifetime = 5f;     

    private Rigidbody2D rb;
    private float startTime;
        
    private Transform target;    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;
                
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            target = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player GameObject not found!");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Time.time - startTime >= lifetime)
        {
            Destroy(gameObject);
        }
        else
        {
            if (target != null)
            {
                
                Vector2 direction = (Vector2)target.position - rb.position;
                direction.Normalize();

               
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                
                rb.rotation = angle;

               
                rb.velocity = direction * speed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            Debug.Log("Missile hit the player!");

            SelfDestroy();
        }
        else if (collision.CompareTag("Missile"))
        {
            SelfDestroy();
        }
        
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
