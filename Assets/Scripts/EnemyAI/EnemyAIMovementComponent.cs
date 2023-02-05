using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.EnemyAI
{
    using CodeBase.StackComponent;
    using CodeBase.Managers.ObjectPoolManager;
    using CodeBase.Managers.GameManager;
    public class EnemyAIMovementComponent : MonoBehaviour
    {
        private const float _kFixedHeight = .5f;
        private const float _kAIDistanceCheck = 0.3f;
        public bool isGameStarted;
        [SerializeField] float _movementSpeed;
        [SerializeField] GameObject _finishGate;
        private List<StackComponent> _targetList = new List<StackComponent>();
        private GameObject _targetObject;
        private Vector3 _fixedTargetPosition;
        private Rigidbody _aiRigidbody;
        private void Awake()
        {
            GameManager.GameStateChanged += OnGameStateChanged;
        }
        private void OnDestroy()
        {
            GameManager.GameStateChanged -= OnGameStateChanged;
        }
        private void OnGameStateChanged(GameState state)
        {
            isGameStarted = (state == GameState.EventOne) ? true : false;
        }

        void Start()
        {
            _aiRigidbody = GetComponent<Rigidbody>();
            _targetList = ObjectPool.Instance._redBoxList;
            SelectTarget();
        }

        void Update()
        {
            if (isGameStarted)
                MoveToTarget();

        }
        private void MoveToTarget()
        {
            if (_targetObject != null)
            {
                transform.LookAt(_fixedTargetPosition);
                transform.position = Vector3.MoveTowards(transform.position, _fixedTargetPosition, _movementSpeed * Time.deltaTime);
            }
            if (Vector3.Distance(_targetObject.transform.position, _aiRigidbody.position) < _kAIDistanceCheck)
            {
                _targetList.Remove(_targetObject.GetComponent<StackComponent>());
                SelectTarget();
            }
        }
        private void SelectTarget()
        {
            if (_targetList.Count > 0)
            {
                _targetObject = _targetList[Random.Range(0, _targetList.Count)].gameObject;
                _fixedTargetPosition = new Vector3(_targetObject.transform.position.x, _kFixedHeight, _targetObject.transform.position.z);
            }
            else
            {
                _targetObject = _finishGate;
                _fixedTargetPosition = new Vector3(_targetObject.transform.position.x, _kFixedHeight, _targetObject.transform.position.z);
            }
        }
    }
}
