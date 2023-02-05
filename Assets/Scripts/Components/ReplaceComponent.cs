using UnityEngine;

namespace CodeBase.ReplaceComponent
{
    using CodeBase.StackComponent;
    using CodeBase.Managers.ObjectPoolManager;
    public class ReplaceComponent : MonoBehaviour, IReplaceable
    {
        private const float _kDistanceCheck= -.95f;
        private Vector3 _startPosition;
        private GameObject _rObject;

        private void Start()
        {
            _startPosition = transform.position;
        }
        public void ReplaceCheck()
        {
            if (_rObject != null)
                Replace();
            else
                SetDefaults();
        }
        public void Replace()
        {
            transform.position = _rObject.transform.position;
            _rObject.transform.position = _startPosition;
            this.enabled = false;
            if (transform.position.z< _kDistanceCheck)
            {
                this.gameObject.layer = LayerMask.NameToLayer("Default");
                ObjectPool.Instance.CheckColumn();
            }
            else
            {
                _startPosition = transform.position;
            }
        }
        private void SetDefaults()
        {
            transform.position = _startPosition;
            this.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<StackComponent>() != null)
            {
                _rObject = other.gameObject;
            }
        }
    }
}
