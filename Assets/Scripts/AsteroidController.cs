using UnityEngine;
public class AsteroidController : MonoBehaviour
{
    public AsteroidController asteroidPrefab;

    [SerializeField] private int _newAsteroidsToSpawn = 4;
    [SerializeField] private float _spawnRadius = 2f;
    [SerializeField] private float _moveSpeed = 15f;
    
    public float size = 1f;
    public float maxSize = 2f;
    public float minSize = 0.5f;
    
    private float _health = 1f;
    private Rigidbody _rb;
    
    private void Awake()
    { 
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        FindObjectOfType<GameManager>().asteroidsInSceneList.Add(gameObject);
        
        transform.localScale = Vector3.one * size;
        _rb.mass = size;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(1);
            TakeDamage(1);
        }
    }
    
    public void TakeDamage(int damage)
    {
        _health -= damage;
        FindObjectOfType<AudioManager>().Play("Hit");
        FindObjectOfType<GameManager>().Score(1);

        if (_health == 0)
        {
            if (size * 0.5 > minSize)
            {
                InstanceCircle(_newAsteroidsToSpawn);
            }
            Destroy(gameObject);
        }
    }
    
    public void Trajectory(Vector3 direction)
    {
        _rb.AddForce(direction  * _moveSpeed, ForceMode.Force);
    }
    
    private void InstanceCircle(int numberOfObjects)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            float x = Mathf.Cos(angle) * _spawnRadius;
            float z = Mathf.Sin(angle) * _spawnRadius;
            Vector3 pos = transform.position + new Vector3(x, 0, z);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            
            AsteroidController newAsteroid = Instantiate(asteroidPrefab, pos, rot);
            newAsteroid.size = size * 0.5f;
            newAsteroid.Trajectory(newAsteroid.transform.right * _moveSpeed);
        }
    }
}
