using UnityEngine;

public interface ICharacterShooter
{
    Vector2 direction { get; set; }
    void Shoot();
    void CanShoot();
}
