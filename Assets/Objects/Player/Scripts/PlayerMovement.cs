using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputControls actions;
    private Rigidbody2D rb;
    public float speed;
    

    void Awake()
    {
        actions = new InputControls();
    }
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
