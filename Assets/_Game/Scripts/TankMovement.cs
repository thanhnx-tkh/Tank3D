using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private Rigidbody rb;
    [SerializeField] public float rotationSpeed = 1000f;
    public AudioSource MoveTankAudio;
    public float speed = 12f;
    private Vector3 direction;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start() {
        MoveTankAudio.enabled = false;
    }
    private void OnEnable()
    {
        rb.isKinematic = false;
    }
    private void OnDisable()
    {
        rb.isKinematic = true;
    }
    private void Update()
    {
        direction = new Vector3(joystick.Horizontal, joystick.Vertical, 0);
    }
    private void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        if (direction.magnitude >= 0.1f)
        {
            MoveTankAudio.enabled = true;
            Vector3 moveDirection = new Vector3(direction.x, 0f, direction.y);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            float angleDifference = Quaternion.Angle(transform.rotation, targetRotation);

            if (angleDifference < 5f)
            {
                Vector3 moveVector = transform.forward * speed;
                rb.velocity = moveVector;
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            MoveTankAudio.enabled = false;
            rb.velocity = Vector3.zero;
        }
    }
}