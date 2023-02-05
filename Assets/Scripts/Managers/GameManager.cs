using UnityEngine;
using System;

namespace CodeBase.Managers.GameManager
{
    using CodeBase.EnemyAI;
    using CodeBase.MovementComponent;
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public static event Action<GameState> GameStateChanged;
        public GameState State;
        [SerializeField] private EnemyAIMovementComponent _enemy;
        [SerializeField] private MovementComponent _player;
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            UpdateGameState(GameState.WaitToStart);
        }
        public void UpdateGameState(GameState newState)
        {
            State = newState;
            if (State == GameState.EventTwo)
            {
                PlayerCanMoveObjects();
            }
            GameStateChanged?.Invoke(newState);
        }

        private void PlayerCanMoveObjects()
        {
            Destroy(_enemy.gameObject);
            Destroy(_player.gameObject);
        }
    }
    public enum GameState
    {
        WaitToStart,
        EventOne,
        EventTwo,
        Finish,
    }
}
