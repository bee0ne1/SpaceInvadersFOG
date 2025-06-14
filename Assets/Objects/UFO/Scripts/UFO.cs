using UnityEngine;

public class UFO : MonoBehaviour
{
    [Header("Configuração")]
    public float speed;
    public AudioClip destroySound;
    public float screenOffset = 1f;

    private Animator animator;
    private AudioSource audioSource;
    
    private Vector2 direction;
    private bool isActive = true;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
        SetStartingPositionAndDirection();
    }

    private void Update()
    {
        if (isActive)
            transform.Translate(direction * speed * Time.deltaTime);
        
        // Auto-destruir caso saia muito da tela
        if (Mathf.Abs(transform.position.x) > Camera.main.orthographicSize * Camera.main.aspect + screenOffset + 1f)
        {
            isActive = false;
            GameManager.Instance.ClearUFO();
            Destroy(gameObject);
        }
    }

    public void DestroyUFO()
    {
        if (!isActive) return;
        StartCoroutine(DestroyUFORoutine());
    }

    private System.Collections.IEnumerator DestroyUFORoutine()
    {
        animator.Play("dying");

        if (destroySound)
            audioSource.PlayOneShot(destroySound);

        isActive = false;

        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.ClearUFO();

        gameObject.SetActive(false);
    }

    private void SetStartingPositionAndDirection()
    {
        float screenY = Camera.main.orthographicSize;
        float screenX = screenY * Camera.main.aspect;

        bool fromLeft = Random.value < 0.5f;

        if (fromLeft)
        {
            transform.position = new Vector3(-screenX - screenOffset, screenY - 0.5f, 0);
            direction = Vector2.right;
        }
        else
        {
            transform.position = new Vector3(screenX + screenOffset, screenY - 0.5f, 0);
            direction = Vector2.left;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
            DestroyUFO();
    }
}
