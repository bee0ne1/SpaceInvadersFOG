using UnityEngine;

public interface ICharacterStats
{
    int life { get; set; }
    bool dead { get; set; }

    void Death();
}
