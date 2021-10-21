using System.Drawing;
using UnityEngine;
using Random = UnityEngine.Random;
public class EnemyController : MonoBehaviour
{
    private Rigidbody rb;
    private Transform target;
    private Vector3 direction;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSize = 4f;
    [SerializeField]private float minSize = 0.5f;
    private float health = 1f;
    private float size;


    [SerializeField] private float instanceRadius = 2f;


    private void Awake()
    { 
        rb = GetComponent<Rigidbody>();

    }

    private void Start()
    {
        size = Random.Range(minSize, maxSize);
        transform.localScale = Vector3.one * size;
        // rb.mass = size;
        // target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        // direction = (target.position - transform.position).normalized * moveSpeed;
        // rb.velocity = new Vector3(direction.x, 0, direction.z);
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (size>=minSize)
            {
                InstanceCircle(6);
            }
            Destroy(gameObject);
        }
    }

    public void InstanceCircle(int numberOfObjects)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            float x = Mathf.Cos(angle) * instanceRadius;
            float z = Mathf.Sin(angle) * instanceRadius;
            Vector3 pos = transform.position + new Vector3(x, 0, z);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            GameObject go = Instantiate(gameObject, pos, rot);
            
            go.transform.localScale *= 0.5f;

        }
    }
}
