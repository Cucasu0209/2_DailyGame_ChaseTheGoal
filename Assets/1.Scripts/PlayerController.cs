using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    private Animator PlayerAnimator;
    [HideInInspector] public CharacterController CharController;

    [Header("Gravity")]
    [SerializeField] public float Gravity = -50;
    public readonly float DefaultGravity = -50;
    [HideInInspector] public Vector3 Velocity;
    [SerializeField] private LayerMask GroundMask;
    [SerializeField] private float GroundYOffset;

    [Header("Movement and Aiming")]
    public Transform ForwardAxis;



    #region Monobehaviour
    private void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
        CharController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        ApplyGravity();
    }
    #endregion

    #region Animation
    float LastSpeed = 0;
    public void SetSpeed(float speed)
    {
        LastSpeed = Mathf.Lerp(LastSpeed, speed, 15 * Time.deltaTime);
        PlayerAnimator.SetFloat("speed", LastSpeed);
    }
    public void SetJumpAnim()
    {
        PlayerAnimator.SetTrigger("jump");
    }
    #endregion

    #region Gravity
    public bool IsGrounded()
    {
        Vector3 SpherePos = transform.position - Vector3.up * GroundYOffset;
        if (Physics.CheckSphere(SpherePos, CharController.radius - 0.05f, GroundMask))
        {
            return true;
        }
        return false;
    }
    void ApplyGravity()
    {
        if (IsGrounded() == false)
        {
            Velocity.y += Gravity * Time.deltaTime;
        }
        else if (Velocity.y < 0)
        {
            Velocity.y = 0;


        }

        CharController.Move(Velocity * Time.deltaTime);
    }
    #endregion
}
