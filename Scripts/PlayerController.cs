using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private float m_movementSpeed = 2.5f;
    private float m_xMovement;
    private float m_healthPoints = 100;
    [Range(0, .3f)]
    [SerializeField]
    private float m_movementSmoothing = .05f; // How much to smooth out the movement

    [SerializeField] private float m_jumpForce = 20f;
    //[SerializeField] private float m_knockBackForce = 20;

    [SerializeField] private float m_slopeCheckDistance;
    [SerializeField] private float m_groundedRadius = .2f; // Radius of the overlap circle to determine if grounded

    private float m_slopeDownAngle;
    private float m_lastSlopeAngle;
    private float m_slopeSideAngle;


    //private int m_gold = 0;

    private bool m_isJumping;
    private bool m_isOnGround;
    private bool m_canJump;
    private bool m_facingRight = true;

    private Vector3 m_velocity = Vector3.zero;

    private Vector2 m_newVelocity;
    private Vector2 m_newForce;
    private Vector2 m_colliderSize;


    private Rigidbody2D m_rigidbody2D;

    private CapsuleCollider2D m_capsuleCollider;

    [SerializeField] private PhysicsMaterial2D m_noFriction;
    [SerializeField] private PhysicsMaterial2D m_fullFriction;

    [SerializeField]
    private LayerMask m_whatIsGround = new LayerMask(); // A mask determining what is ground to the character

    [SerializeField]
    private Transform m_groundCheck = null; // A position marking where to check if the player is grounded.

    private enum State
    {
        Idle,
        Run,
        Jump,
        Attack,
        Hurt,
        Die
    };

    private State m_state = State.Idle;

    private void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        CheckGround();
        Move();
    }

    private void Update()
    {
        UpdateState();
        CheckInput();
        //Interactions();
    }

    private void CheckInput()
    {
        m_animator.SetBool("isGrounded", m_isOnGround);
        if (m_state != State.Attack)
        {
            m_xMovement = Input.GetAxis("Horizontal") * m_movementSpeed;
            m_animator.SetFloat("Speed", Mathf.Abs(m_xMovement));

            if (m_xMovement > 0 && !m_facingRight) Flip();
            else if (m_xMovement < 0 && m_facingRight) Flip();
            if (Input.GetKey("space"))
            {
                Jump();
            }

            //if (Input.GetButtonDown("Fire1") && m_state != State.Jump && m_state != State.Attack)
            //{
            //    m_xMovement = 0;
            //    m_animator.SetTrigger("attacking");
            //}
        }
    }

    private void Jump()
    {
        if (!m_canJump) return;
        m_animator.SetTrigger("Jump");
        m_animator.SetBool("isJumping", true);
        m_canJump = false;
        m_isJumping = true;
        m_newVelocity.Set(0.0f, 0.0f);
        m_rigidbody2D.velocity = m_newVelocity;
        m_newForce.Set(0.0f, m_jumpForce);
        m_rigidbody2D.AddForce(m_newForce, ForceMode2D.Impulse);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        if (!m_isOnGround)
    //        {
    //            m_animator.SetTrigger("jump");
    //            m_animator.SetBool("jumping", true);
    //            m_rigidbody2D.AddForce((transform.position - collision.gameObject.transform.position).normalized * 7.5f, ForceMode2D.Impulse);
    //        }
    //        else
    //        {
    //            if (transform.position.x < collision.gameObject.transform.position.x)
    //                m_rigidbody2D.velocity = new Vector2(-m_knockBackForce, m_rigidbody2D.velocity.y);
    //            else
    //                m_rigidbody2D.velocity = new Vector2(m_knockBackForce, m_rigidbody2D.velocity.y);
    //            TakeDamage(5);
    //        }
    //    }
    //}

    private void CheckGround()
    {
        m_isOnGround = Physics2D.OverlapCircle(m_groundCheck.position, m_groundedRadius, m_whatIsGround);
        Debug.Log(m_isOnGround);
        if (m_rigidbody2D.velocity.y <= 0.0f)
        {
            m_animator.SetBool("isJumping", false);
            m_isJumping = false;
        }

        if (m_isOnGround && !m_isJumping) m_canJump = true;

    }

    private void Move()
    {
        Debug.Log(m_isOnGround);
        if (m_isOnGround)
        { // On Ground
            m_newVelocity.Set(m_movementSpeed * m_xMovement, 0.0f);
            m_rigidbody2D.velocity = Vector3.SmoothDamp(m_rigidbody2D.velocity, m_newVelocity, ref m_velocity,
                m_movementSmoothing);
        }
        else if (!m_isOnGround)
        { // In the air
            m_newVelocity.Set(m_movementSpeed * m_xMovement, m_rigidbody2D.velocity.y);
            m_rigidbody2D.velocity = Vector3.SmoothDamp(m_rigidbody2D.velocity, m_newVelocity, ref m_velocity,
                m_movementSmoothing);
        }

       
    }

    //private void Interactions()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        if (m_nearChest)
    //        {
    //            m_chest.Open();

    //            //m_gold += m_chest.getGold();


    //        }

    //        if (m_nearLever)
    //        {
    //            m_lever.HandleLever();
    //        }
    //    }
    //}

    //public void TakeDamage(int damage)
    //{
    //    m_health -= damage;
    //    if (m_state != State.Hurt)
    //        m_animator.SetTrigger("hurt");
    //    if (m_health <= 0)
    //    {
    //        if (m_state != State.Die)
    //        {
    //            m_animator.SetTrigger("die");
    //            Destroy(gameObject, 5f / 6f);
    //        }
    //    }
    //}

    private void UpdateState()
    {
        AnimatorClipInfo[] currentState = m_animator.GetCurrentAnimatorClipInfo(0);
        string state = currentState[0].clip.name;
        switch (state)
        {
            case "player_idle":
                m_state = State.Idle;
                break;
            case "player_run":
                m_state = State.Run;
                break;
            case "player_jump":
                m_state = State.Jump;
                break;
            case "player_fall":
                m_state = State.Jump;
                break;
            //case "player_attack":
            //    m_state = State.Attack;
            //    break;
            //case "player_hurt":
            //    m_state = State.Hurt;
            //    break;
            //case "player_die":
            //    m_state = State.Die;
            //    break;
            default:
                m_state = State.Idle;
                break;
        }
    }

    private void Flip()
    {
        m_facingRight = !m_facingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}

