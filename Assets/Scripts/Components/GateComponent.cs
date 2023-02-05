using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.GateComponent
{
    using CodeBase.Managers.GameManager;
    using CodeBase.Managers.ObjectPoolManager;
    public class GateComponent : MonoBehaviour, IGate
    {
        [SerializeField] private bool isPlayer;
        private Vector3 _secondScale = new Vector3(1, 3, 1);
        private Vector3 _playerGateSecondPos = new Vector3(8, 1.5f, -1);
        private Vector3 _enemyGateSecondPos = new Vector3(8, 1.5f, 1);
        private Quaternion _secondRotation = new Quaternion(0, 90, 0, 0);
        private void Awake()
        {
            GameManager.GameStateChanged += SetNewPosition;
        }
        private void OnDestroy()
        {
            GameManager.GameStateChanged -= SetNewPosition;
        }
        public void ScoreCheck()
        {
            if (isPlayer && ObjectPool.Instance._blueBoxList.Count == 0)
            {
                GameManager.Instance.UpdateGameState(GameState.EventTwo);
            }
            else if (!isPlayer && ObjectPool.Instance._redBoxList.Count == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        private void SetNewPosition(GameState state)
        {
            if (state == GameState.EventTwo)
            {
                if (isPlayer)
                    transform.position = _playerGateSecondPos;
                else
                    transform.position = _enemyGateSecondPos;

                transform.localScale = _secondScale;
                transform.rotation = _secondRotation;
            }
        }

    }
}
