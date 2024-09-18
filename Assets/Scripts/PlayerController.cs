using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float maxForwardSpeed = 8f;
    public float gravity = 20f;
    public float jumpSpeed = 10f;
    private bool idle;
    protected bool m_IsGrounded = true;
    protected bool m_ReadyToJump;
    protected float m_DesiredForwardSpeed;
    protected float m_ForwardSpeed;
    protected float m_VerticalSpeed;
    protected Vector2 m_MoveInput;
    protected bool m_JumpInput;
    protected CharacterController m_CharCtrl;
    private InputManagement inputManager;
    
    const float k_GroundedRayDistance = 1f;
    const float k_JumpAbortSpeed = 10f;
    const float k_StickingGravityProportion = 0.3f;
    const float k_GroundAcceleration = 20f;
    const float k_GroundDeceleration = 25f;

    void Awake()
    {
        m_CharCtrl = GetComponent<CharacterController>();
        inputManager = FindObjectOfType<InputManagement>();
        inputManager.OnMovementInput += HandleMovementInput;
        /*inputManager.OnInteractionInput += HandleJumpInput;*/
    }
    void FixedUpdate()
    {
        if (!idle)
        {
            CalculateForwardMovement();
            CalculateVerticalMovement();

            Vector3 movement = new Vector3(m_MoveInput.x * m_ForwardSpeed, m_VerticalSpeed,
                m_MoveInput.y * m_ForwardSpeed);
            m_CharCtrl.Move(movement * Time.deltaTime);
        }
    }

    void CalculateForwardMovement()
    {
        if (m_MoveInput.sqrMagnitude > 1f)
            m_MoveInput.Normalize();

        m_DesiredForwardSpeed = m_MoveInput.magnitude * maxForwardSpeed;
        float acceleration = m_MoveInput.sqrMagnitude > 0 ? k_GroundAcceleration : k_GroundDeceleration;
        m_ForwardSpeed = Mathf.MoveTowards(m_ForwardSpeed, m_DesiredForwardSpeed, acceleration * Time.deltaTime);
    }

    void CalculateVerticalMovement()
    {
        if (!m_JumpInput && m_IsGrounded)
            m_ReadyToJump = true;

        if (m_IsGrounded)
        {
            m_VerticalSpeed = -gravity * k_StickingGravityProportion;

            if (m_JumpInput && m_ReadyToJump)
            {
                m_VerticalSpeed = jumpSpeed;
                m_IsGrounded = false;
                m_ReadyToJump = false;
            }
        }
        else
        {
            if (!m_JumpInput && m_VerticalSpeed > 0.0f)
            {
                m_VerticalSpeed -= k_JumpAbortSpeed * Time.deltaTime;
            }

            if (Mathf.Approximately(m_VerticalSpeed, 0f))
            {
                m_VerticalSpeed = 0f;
            }

            m_VerticalSpeed -= gravity * Time.deltaTime;
        }
    }

    void HandleMovementInput(Vector2 input)
    {
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            m_MoveInput = new Vector2(input.x, 0);
        }
        else
        {
            m_MoveInput = new Vector2(0, input.y);
        }
    }

    void HandleJumpInput(float input)
    {
        m_JumpInput = input > 0;
    }

    void OnDestroy()
    {
        inputManager.OnMovementInput -= HandleMovementInput;
        inputManager.OnInteractionInput -= HandleJumpInput;
    }
    public void ToggleMovement()
    {
        idle = !idle;
    }
}