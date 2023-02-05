using UnityEngine;

namespace CodeBase.TriggerComponent
{
    public class TriggerComponent : MonoBehaviour
    {
        [SerializeField] private bool _isPlayer;
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<IStackable>() != null)
            {
                other.GetComponent<IStackable>().Stack(_isPlayer);
            }
            if (other.GetComponent<IGate>()!=null)
            {
                other.GetComponent<IGate>().ScoreCheck();
            }
        }
    } 
}
