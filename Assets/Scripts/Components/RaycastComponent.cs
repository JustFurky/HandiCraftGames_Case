using UnityEngine;

namespace CodeBase.RaycastComponent
{
    using CodeBase.ReplaceComponent;
    using CodeBase.Managers.GameManager;
    public class RaycastComponent : MonoBehaviour
    {
        private const float _kSecondPosTime = .05f;
        private const float _kSecondRotTime = .025f;
        private const float _kSelectedObjectHeight = 0.25f;
        [SerializeField] private LayerMask _layerMask;
        private Camera _mainCam;
        private GameObject _selectedObject;
        private bool isSeconStateActive;
        private Vector3 _startPos;
        private Vector3 _cSecondEventPosition = new Vector3(0, 20, 0);
        private void Awake()
        {
            _layerMask = LayerMask.GetMask("BlueBox");
            _mainCam = GetComponent<Camera>();
            GameManager.GameStateChanged += OnStateChanged;
        }
        private void OnDestroy()
        {
            GameManager.GameStateChanged -= OnStateChanged;
        }
        private void OnStateChanged(GameState state)
        {
            isSeconStateActive = (state == GameState.EventTwo) ? true : false;
        }

        void Update()
        {
            StateCheck();
        }

        private void StateCheck()
        {
            if (isSeconStateActive)
            {
                if (transform.position != _cSecondEventPosition)
                {
                    transform.position = Vector3.MoveTowards(transform.position, _cSecondEventPosition, _kSecondPosTime);
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90, 0, -90), _kSecondRotTime);
                }
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastSender();
                }
                else if (Input.GetMouseButtonUp(0) && _selectedObject)
                {
                    _selectedObject.GetComponent<IReplaceable>().ReplaceCheck();
                    _selectedObject = null;
                }
                if (_selectedObject != null && Input.GetMouseButton(0))
                {
                    MoveObject();
                }
            }
        }

        private void RaycastSender()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
            {
                _selectedObject = hit.collider.gameObject;
                _startPos = Input.mousePosition - _mainCam.WorldToScreenPoint(_selectedObject.transform.position);
                _selectedObject.GetComponent<ReplaceComponent>().enabled = true;
                
            }
        }
        private void MoveObject()
        {
            Vector3 pos;
            pos = _mainCam.ScreenToWorldPoint(Input.mousePosition - _startPos);
            _selectedObject.transform.position = new Vector3(_selectedObject.transform.position.x,
                _kSelectedObjectHeight,
                Mathf.Clamp(pos.z, -2, 2));
        }
    }
}
