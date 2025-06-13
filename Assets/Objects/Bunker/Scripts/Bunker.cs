using System;
using UnityEngine;

public class Bunker : MonoBehaviour
{
    public Sprite[] states;

    private int health;
    private BoxCollider2D box;
    private SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 4;
        box = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            health--;
            if (health <= 0)
                Destroy(gameObject);
            else
                sr.sprite = states[health - 1];

            if (sr.sprite == states[1])
            {
                box.size = new Vector2(0.13f, 0.08f);
                box.offset = box.offset + new Vector2(0, -0.02f);
            }

            if (sr.sprite == states[0])
            {
            box.size = new Vector2(0.13f, 0.05f);
            box.offset = box.offset + new Vector2(0, -0.04f);
            }

    }
    }
}
