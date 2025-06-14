using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputControls actions;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public float speed;
    
    void Awake()
    {
        actions = new InputControls();
        actions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        actions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }
    
    void OnEnable()
    {
        actions.Player.Enable();
    }

    void OnDisable()
    {
        actions.Player.Disable();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;
    }
    

    
}
