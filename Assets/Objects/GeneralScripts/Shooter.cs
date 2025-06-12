using UnityEngine;

public abstract class Shooter : MonoBehaviour, ICharacterShooter 
{
    public Vector2 direction { get; set; }
    
    public Bullet bulletPrefab;
    public Transform bulletspawn; 
    
    protected Bullet SpawnBullet()
    {
        Bullet newBullet = Instantiate(bulletPrefab, bulletspawn.position, bulletspawn.rotation);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = direction*newBullet.speed;
        return newBullet;
    }
    

    public abstract void Shoot();
    public abstract void CanShoot();

}
