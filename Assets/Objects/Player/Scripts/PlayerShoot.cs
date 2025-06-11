using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerShoot : Shooter 
{
    private InputControls actions;
    private Bullet currentBullet;
    void Awake()
    {
        actions = new InputControls();
        actions.Player.Shoot.performed += ctx => TryShoot();
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
        direction = Vector2.up;
    }
    
    void TryShoot()
    {
        if (currentBullet == null)
            Shoot();
    }
    
    public override void Shoot()
    {
        currentBullet = SpawnBullet();
    }

    
}
