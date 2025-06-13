using UnityEngine;
using System.Collections;
public class UFO : MonoBehaviour
{
    public float speed = 5f;
    public float minSpawnTime = 10f;
    public float maxSpawnTime = 30f;
    public int bonusPoints = 100;
    public AudioClip spawnSound;
    public AudioClip destroySound;

    private Vector3 leftOffscreen = new Vector3(-10f, 4f, 0);
    private Vector3 rightOffscreen = new Vector3(10f, 4f, 0);
    private bool movingRight;
    private AudioSource audioSource;
    private bool isActive = false;
    private Animator animator;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(SpawnRoutine());
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isActive) return;

        transform.Translate((movingRight ? Vector3.right : Vector3.left) * speed * Time.deltaTime);

        // Se saiu da tela, desativa
        if (movingRight && transform.position.x > 10f ||
            !movingRight && transform.position.x < -10f)
        {
            gameObject.SetActive(false);
            isActive = false;
            StartCoroutine(SpawnRoutine());
        }
    }

    private System.Collections.IEnumerator SpawnRoutine()
    {
        float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
        yield return new WaitForSeconds(waitTime);

        movingRight = Random.value > 0.5f;
        transform.position = movingRight ? leftOffscreen : rightOffscreen;
        gameObject.SetActive(true);
        isActive = true;

        if (spawnSound)
            audioSource.PlayOneShot(spawnSound);
    }

    public void DestroyUFO()
    {
        StartCoroutine(DestroyUFORoutine());
    }

    private IEnumerator DestroyUFORoutine()
    {
        animator.Play("dying");
        if (destroySound) audioSource.PlayOneShot(destroySound);

        isActive = false;

        yield return new WaitForSeconds(0.2f); // Espera a animação

        StartCoroutine(SpawnRoutine()); // Pode rodar antes de desativar

        yield return null; // Espera um frame se quiser garantir

        gameObject.SetActive(false); // Só desativa no final
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            DestroyUFO();
        }
    }
}
