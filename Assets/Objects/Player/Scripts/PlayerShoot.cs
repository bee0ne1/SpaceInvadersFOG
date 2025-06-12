using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerShoot : Shooter 
{
    private InputControls actions;
    private Bullet currentBullet;
    void Awake()
    {
        actions = new InputControls();
        actions.Player.Shoot.performed += ctx => CanShoot();
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
    
    public override void CanShoot()
    {
        if (currentBullet == null)
            Shoot();
    }
    
    public override void Shoot()
    {
        currentBullet = SpawnBullet();
    }

    
}
