using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator m_animator;

    [SerializeField] private float m_movementSpeed = 2.5f;
    [SerializeField] private Transform m_rightPoint;
    [SerializeField] private Transform m_leftPoint;

    
    private bool m_facingRight = true;
    private float m_xMovement = -1;
    private float m_healthPoints = 100;

    private Rigidbody2D m_rigidbody2D;
    private Transform m_player; 
    private CapsuleCollider2D m_capsuleCollider;

    private Vector2 m_Velocity;

    private enum State
    {
        Walk,
        Run,
        Attack,
        Hurt,
        Die
    };

    private State m_state = State.Walk;

    private void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").transform; 
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_capsuleCollider = GetComponent<CapsuleCollider2D>();
        m_animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (m_player.position.x > m_leftPoint.position.x && m_player.position.x < m_rightPoint.position.x)
        {
            Angry();
        }
        else Move();
    }

    //private void Update()
    //{
    //    UpdateState();
    //    //Interactions();
    //}

    private void UpdateState()
    {
        AnimatorClipInfo[] currentState = m_animator.GetCurrentAnimatorClipInfo(0);
        string state = currentState[0].clip.name;
        switch (state)
        {
            case "wDemon_walk":
                m_state = State.Walk;
                break;
            case "wDemor_run":
                m_state = State.Run;
                break;
            //case "player_jump":
            //    m_state = State.Jump;
            //    break;
            //case "player_fall":
            //    m_state = State.Jump;
            //    break;
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
                m_state = State.Walk;
                break;
        }
    }

    private void Move()
    {
        m_movementSpeed = 2.5f;
        m_animator.SetBool("isAngry", false);
        UpdateState();
        if (gameObject.transform.position.x > m_rightPoint.position.x) m_xMovement = -1;
        else if (gameObject.transform.position.x < m_leftPoint.position.x) m_xMovement = 1;

        m_Velocity.Set(m_movementSpeed * m_xMovement, 0.0f);
        m_rigidbody2D.velocity = m_Velocity;
        if (m_xMovement > 0 && !m_facingRight) Flip();
        else if (m_xMovement < 0 && m_facingRight) Flip();
    }

    private void Angry()
    {
        m_movementSpeed = 10f;
        m_animator.SetBool("isAngry", true);
        UpdateState();
        if (gameObject.transform.position.x > m_player.position.x) m_xMovement = -1;
        else if (gameObject.transform.position.x < m_player.position.x) m_xMovement = 1;

        transform.position = Vector2.MoveTowards(transform.position, m_player.position, m_movementSpeed * Time.deltaTime);
        if (m_xMovement > 0 && !m_facingRight) Flip();
        else if (m_xMovement < 0 && m_facingRight) Flip();
    }



    private void Flip()
    {
        m_facingRight = !m_facingRight;

        transform.Rotate(0f, 180f, 0f);
    }


}