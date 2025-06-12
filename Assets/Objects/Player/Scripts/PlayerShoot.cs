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
        direction = Vector2.up;
    }
    
    void OnEnable()
    {
        actions.Player.Enable();
    }

    void OnDisable()
    {
        actions.Player.Disable();
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
