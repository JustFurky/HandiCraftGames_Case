using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Managers.ObjectPoolManager
{
    using CodeBase.Managers.GameManager;
    using CodeBase.StackComponent;
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool Instance;

        private const float _kPosZCheck = -.95f;

        public List<StackComponent> _redBoxList = new List<StackComponent>();
        public List<StackComponent> _blueBoxList = new List<StackComponent>();

        [SerializeField] private int _maxAmountPerPrefab;
        [SerializeField] private StackComponent _redBoxPrefab;
        [SerializeField] private StackComponent _blueBoxPrefab;

        [SerializeField] private Vector3 _startPRed;
        [SerializeField] private Vector3 _startPBlue;

        private int _lastSelectedNumber = 0;
        private void Awake()
        {
            Instance = this;
            CreateBoxes();
            GameManager.GameStateChanged += Clean;
        }

        public void CreateBoxes()
        {
            CreatePlayersBoxes();
            CreateAIBoxes();
        }

        public void RemoveFromList(StackComponent obj, bool isBlue)
        {
            if (isBlue)
                _blueBoxList.Remove(obj);
            else
                _redBoxList.Remove(obj);


            Destroy(obj);
        }
        public void CheckColumn()
        {
            int _rightOrder=0;
            for (int i = 0; i < _blueBoxList.Count; i++)
            {
                if (_blueBoxList[i].gameObject.transform.position.z < _kPosZCheck)
                    _rightOrder++;
            }
            if (_rightOrder == 5)
                GameManager.Instance.UpdateGameState(GameState.Finish);
        }

        private void CreatePlayersBoxes()
        {
            for (int i = 0; i < _maxAmountPerPrefab; i++)
            {
                StackComponent _currentBlue = Instantiate(_blueBoxPrefab);
                _currentBlue.Initialize(true, _startPBlue);
                _startPBlue.x += 2;
                _startPBlue.z = GetRandomPosition();
                _blueBoxList.Add(_currentBlue);
                _currentBlue.transform.parent = this.transform;
            }
        }

        private void CreateAIBoxes()
        {
            for (int i = 0; i < _maxAmountPerPrefab; i++)
            {
                StackComponent _currentRed = Instantiate(_redBoxPrefab);
                _currentRed.Initialize(false, _startPRed);
                _startPRed.x += 2;
                _startPRed.z = GetRandomPosition();
                _redBoxList.Add(_currentRed);
                _currentRed.transform.parent = this.transform;
            }
        }
        private void Clean(GameState state)
        {
            if (state==GameState.EventTwo)
            {
                _startPBlue.x = -4;
                _startPRed.x = -4;
                _redBoxList.Clear();
                _blueBoxList.Clear();
                for (int i = 0; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
                CreateBoxes();
            }
        }

        int GetRandomPosition()
        {
            int randomSelectedNumber = (Random.Range(0, 100) >= 50) ? 1 : -1;
            if (randomSelectedNumber == _lastSelectedNumber)
                randomSelectedNumber = -_lastSelectedNumber;
            _lastSelectedNumber = randomSelectedNumber;
            return randomSelectedNumber;
        }
    }
}
