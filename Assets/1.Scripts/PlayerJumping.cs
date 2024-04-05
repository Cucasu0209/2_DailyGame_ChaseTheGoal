using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : MonoBehaviour
{
    [SerializeField] private float JumpForce = 10;
    private PlayerController Controller;
    private void Start()
    {
        Controller = GetComponent<PlayerController>();
        UserInputController.Instance.OnJumpBtnClick += Jump;
    }
    private void OnDestroy()
    {
        UserInputController.Instance.OnJumpBtnClick -= Jump;
    }
    private void Jump()
    {
        Controller.Velocity += Vector3.up * (JumpForce - Controller.Velocity.y);
        Controller.SetJumpAnim();
    }
}
