using UnityEngine;

namespace CodeBase.MovementComponent
{
    public class MovementComponent : MonoBehaviour, IMovementAttach
    {
        [SerializeField] private FloatingJoystick _playerJoystick;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        private Rigidbody _playerRigidBody;
        private void Start()
        {
            _playerRigidBody = GetComponent<Rigidbody>();
        }
        public void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Attach(new Vector2(_playerJoystick.Horizontal, _playerJoystick.Vertical));
            }
        }
        public void Attach(Vector2 TargetPosition)
        {
            _playerRigidBody.position += new Vector3(TargetPosition.x, 0, TargetPosition.y) * _moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(Vector3.up, new Vector3(TargetPosition.x, 0, TargetPosition.y));
        }
    } 
}
