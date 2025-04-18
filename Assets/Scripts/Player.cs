using UnityEngine;

public class Player : MonoBehaviour
{
    private float _speed = 3f;
    private bool _moveLeft = false;
    private bool _moveRight = false;
    private AudioSource _audioSource;

    [SerializeField]
    private float jumpForce = 400f;  

    private Rigidbody2D rb;
    private Transform groundCheck;
    private bool isGrounded;
    
    [SerializeField] 
    LayerMask layerMask;

    [SerializeField]
    private AudioClip audioCoin = null;

    public GameManagerScript gameManager;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
        
        rb.gravity = 1f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, layerMask);

        _moveLeft = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        _moveRight = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        { 
            Vector2 currentVelocity = rb.linearVelocity;
            currentVelocity.y = 0f;
            rb.linearVelocity = currentVelocity;
            
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        float moveAmount = _speed * Time.fixedDeltaTime;

        Vector2 move = Vector2.zero;


        if (_moveLeft)
        {
            move.x += moveAmount;
        }

        if (_moveRight)
        {
            move.x -= moveAmount;
        }


        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        if (moveMagnitude > moveAmount)
        {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }

        pos += move;


        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("pic"))
        {
            Destroy(gameObject);
            gameManager.GameOver();
            Debug.Log("Dead");
        }

        if (collision.CompareTag("coin"))
        {
            _audioSource.PlayOneShot(audioCoin);
        }
    }
}
