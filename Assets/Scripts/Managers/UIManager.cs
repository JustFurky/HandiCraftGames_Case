using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Managers.UIManager
{
    using CodeBase.Managers.GameManager;

    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _startPanel, _finishPanel;
        private void Awake()
        {
            GameManager.GameStateChanged += GameManagerOnStateChanged;
        }
        private void OnDestroy()
        {
            GameManager.GameStateChanged -= GameManagerOnStateChanged;
        }
        private void GameManagerOnStateChanged(GameState state)
        {
            _startPanel.SetActive(state == GameState.WaitToStart);
            _finishPanel.SetActive(state == GameState.Finish);
        }
        public void StartButton()
        {
            GameManager.Instance.UpdateGameState(GameState.EventOne);
        }
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
