using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float Speed = 7;

    private PlayerController Controller;
    private void Start()
    {
        Controller = GetComponent<PlayerController>();
        UserInputController.Instance.OnMovementJoystick += MovePlayer;
    }
    private void OnDestroy()
    {
        UserInputController.Instance.OnMovementJoystick -= MovePlayer;
    }
    private void MovePlayer(float hzInput, float vInput)
    {
        Vector2 MoveInput = new Vector2(hzInput, vInput);
        Vector2 Direction = (new Vector2(hzInput, vInput)).normalized;
        if (MoveInput.magnitude > 0.1f)
        {

            float rotation = Mathf.Atan2(Direction.x, Direction.y) * Mathf.Rad2Deg + Controller.ForwardAxis.eulerAngles.y;
            rotation = (rotation % 360 + 360) % 360;
            float currentRotation = (transform.eulerAngles.y % 360 + 360) % 360;
            if (Mathf.Abs(currentRotation - rotation) > 180)
            {
                if (currentRotation < 180) currentRotation += 360;
                else rotation += 360;
            }
            transform.eulerAngles = Vector3.up * Mathf.Lerp(currentRotation, rotation, 10 * Time.deltaTime);
            Controller.CharController.Move(transform.forward * Speed * Time.deltaTime);
        }
        Controller.SetSpeed(MoveInput.magnitude);
    }
}


