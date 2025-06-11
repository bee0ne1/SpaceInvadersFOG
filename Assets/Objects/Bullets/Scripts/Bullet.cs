using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private float lifetime = 2f;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject,lifetime);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject); 
    }
    
}
