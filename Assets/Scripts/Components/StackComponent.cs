using UnityEngine;
using CodeBase.Managers.ObjectPoolManager;

namespace CodeBase.StackComponent
{
    public class StackComponent : MonoBehaviour, IStackable
    {
        [SerializeField] private bool _isBlue;
        public void Initialize(bool isBlue, Vector3 position)
        {
            _isBlue = isBlue;
            transform.position = position;
        }

        public void Stack(bool isPlayer)
        {
            if (isPlayer != _isBlue)
            {
                return;
            }
            else
            {
                ObjectPool.Instance.RemoveFromList(this, _isBlue);
                this.gameObject.SetActive(false);
            }
        }
    } 
}
