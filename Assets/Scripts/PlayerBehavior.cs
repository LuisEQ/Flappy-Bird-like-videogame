using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    private int _lives = 1;
    private int _points; //Score of the game, every time the bid continues without touching the pipes will receive points
    private float _jumpForce; 

    private PlayerInputActions _playerInputActions;
    private PlayerInput _playerInput;
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField] private float cameraSize;
    private Camera mainCamera;
    private Vector3 currentVelocity = Vector3.zero;

    [SerializeField] private HUD _hudManager;
    [SerializeField] private SpawnManagerPipes _spawnManagerPipes;


    private Animator _animator;

    private float minJumpAngle =-45f;
    private float maxJumpAngle = 45f;
    private float rotationSpeed = 10f;
    private float movementSpeed = 3f;
    private float maxVerticalVelocity = 5f;

    [SerializeField]
    private AudioClip _flyAudio;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _dieSound, _scoreSound;


    // Start is called before the first frame update
    void Start()
    {
        _jumpForce = 10f;
        
        _playerInput = GetComponent<PlayerInput>();
        if(!_playerInput)
            Debug.LogError("PlayerInput is null");

        transform.position = new Vector2(0, 0);
        _playerInputActions = new PlayerInputActions();

        _playerInputActions.Player.Enable();

        _playerInputActions.Player.Jump.performed += Jump;
        mainCamera = Camera.main;
        cameraSize = Camera.main.orthographicSize;

        _animator = GetComponent<Animator>();

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Audio sources is null in the player");
        }
        else
        {
            _audioSource.clip = _flyAudio;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //This doesn't allow the bird to go away the window
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -cameraSize * Screen.width / Screen.height, cameraSize * Screen.width / Screen.height);
        pos.y = Mathf.Clamp(pos.y, -cameraSize + .5f, cameraSize);
        transform.position = pos;

        //It'll make the bird jump in a way more natural than just a normal jump
        float angle = Mathf.Clamp(rb.velocity.y * 2f, minJumpAngle, maxJumpAngle);
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        Vector3 targetPosition = transform.position;
        targetPosition.y = Mathf.Clamp(targetPosition.y, mainCamera.ViewportToWorldPoint(Vector3.zero).y, mainCamera.ViewportToWorldPoint(Vector3.one).y);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, movementSpeed);

        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -maxVerticalVelocity, maxVerticalVelocity));


    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _audioSource.clip = _flyAudio;
            _audioSource.Play();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Point"))
        {
            _points += 1;
            _hudManager.UpdateScore(_points);
            AudioSource.PlayClipAtPoint(_scoreSound, transform.position);
            if(_points%10 == 0)
            {
                //_pipes.ChangeSpeed();
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {

            AudioSource.PlayClipAtPoint(_dieSound, transform.position);
            _lives -= 1;
            _spawnManagerPipes.OnPlayerDeath();
            _animator.SetTrigger("Die");
            GetComponent<Collider2D>().enabled = false;
            _hudManager.UpdateLives(_lives);
        }
    }

}
