using System;
using System.Drawing;
using UnityEngine;
using Random = UnityEngine.Random;
public class AsteroidController : MonoBehaviour
{
    public AsteroidController asteroidPrefab;
    [SerializeField] private int numberToSpawn = 4;
    [SerializeField] private float instanceRadius = 2f;
    
    private Rigidbody rb;
    [SerializeField] private float moveSpeed = 15f;
    
    public float size = 1f;
    public float maxSize = 2f;
    public float minSize = 0.5f;
    private float health = 1f;
    
    private void Awake()
    { 
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        transform.localScale = Vector3.one * size;
        rb.mass = size;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(1);
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        FindObjectOfType<AudioManager>().Play("Hit");
        FindObjectOfType<GameManager>().Score(1);

        if (health == 0)
        {
            if (size * 0.5 > minSize)
            {
                InstanceCircle(numberToSpawn);
            }
            Destroy(gameObject);
        }
    }

    private void InstanceCircle(int numberOfObjects)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            float x = Mathf.Cos(angle) * instanceRadius;
            float z = Mathf.Sin(angle) * instanceRadius;
            Vector3 pos = transform.position + new Vector3(x, 0, z);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            
            AsteroidController newAsteroid = Instantiate(asteroidPrefab, pos, rot);
            newAsteroid.size = size * 0.5f;
            newAsteroid.Trajectory(newAsteroid.transform.right * moveSpeed);
        }
    }

    public void Trajectory(Vector3 direction)
    {
        rb.AddForce(direction  * moveSpeed, ForceMode.Force);
    }
}
