using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] LevelSceneControl _levelControl;
    [SerializeField] GameObject _bodyPrefab;
    public List<GameObject> _snakeSegments = new List<GameObject>();
    public List<int> _snakeSegmentsFooting = new List<int>();
    public bool _canMove = true;
    [SerializeField] LayerMask Blocks;
    [SerializeField] LayerMask InteractiveObjects;

    private void Start()
    {
        transform.forward = Vector3.forward;
        _snakeSegments.Add(gameObject);
        AddBody(); 
    }

    private void Update()
    {
        if (_canMove)
        {
            switch (true)
            {
                case true when (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && transform.forward != Vector3.back && !IsObstacle—heck(gameObject, Vector3.forward):
                    Move(Vector3.forward);
                    break;

                case true when (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && transform.forward != Vector3.right && !IsObstacle—heck(gameObject, Vector3.left):
                    Move(Vector3.left);
                    break;

                case true when (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && transform.forward != Vector3.forward && !IsObstacle—heck(gameObject, Vector3.back):
                    Move(Vector3.back);
                    break;

                case true when (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && transform.forward != Vector3.left && !IsObstacle—heck(gameObject, Vector3.right):
                    Move(Vector3.right);
                    break;
               
                case true when Input.GetKeyDown(KeyCode.Z):
                    AddBody();
                    break;
                
                case true when Input.GetKeyDown(KeyCode.X):
                    RemoveBody();
                    break;
            }
        }
    }

    private void Move(Vector3 moveTo)
    {
        Vector3 _pastPosition = transform.localPosition;
        Quaternion _pastRotation = transform.localRotation;
        Vector3 _temporaryPos;
        Quaternion _temporaryRot;
        ActionInteractiveObject(moveTo);
        foreach (GameObject segment in _snakeSegments)
        {
            if (segment.name == "Head")
            {
                segment.transform.position += moveTo;
                segment.transform.forward = moveTo;
            }
            else
            {
                _temporaryPos = segment.transform.localPosition;
                _temporaryRot = segment.transform.localRotation;
                segment.transform.position = _pastPosition;
                segment.transform.rotation = _pastRotation;
                _pastPosition = _temporaryPos;
                _pastRotation = _temporaryRot;
            }
            if (IsObstacle—heck(segment, Vector3.down)) _snakeSegmentsFooting.Add(1);
            else _snakeSegmentsFooting.Add(0);
        }
        if (_snakeSegmentsFooting.Sum() == 0) FinishGame(false);
        else _snakeSegmentsFooting.Clear();
        GameObject _onFunish = ObstacleCheck(gameObject, Vector3.down, Blocks);
        if (_onFunish && _onFunish.name == "Finish") FinishGame(true);
    }

    private void ActionInteractiveObject(Vector3 rotation)
    {
        GameObject _interactObject = ObstacleCheck(gameObject, rotation, InteractiveObjects);
        Debug.Log(_interactObject);
        if (_interactObject)
        {
            switch (true)
            {
                case true when _interactObject.name == "Apple":
                    Destroy(_interactObject);
                    AddBody();
                    break;
                
                case true when _interactObject.name == "Bomb":
                    Destroy(_interactObject);
                    RemoveBody();
                    break;
                
                case true when _interactObject.name == "Booster":;
                    Dash(_interactObject);
                    break;
            }
        }
    }

    private void AddBody()
    {
        GameObject _last = _snakeSegments[_snakeSegments.Count - 1];
        Vector3 _newPosition = _last.transform.localPosition - _last.transform.forward;
        GameObject _newBody = Instantiate(_bodyPrefab, _newPosition, _last.transform.localRotation, transform.parent);
        _snakeSegments.Add(_newBody);
    }

    private void RemoveBody()
    {
        GameObject _last = _snakeSegments[_snakeSegments.Count - 1];
        _snakeSegments.RemoveAt(_snakeSegments.Count - 1);
        Destroy(_last);
    }

    private void Dash(GameObject Booster)
    {

    }

    private bool IsObstacle—heck(GameObject segment, Vector3 direction)
    {
        if (ObstacleCheck(segment, direction, Blocks)) return true;
        return false;
    }

    private GameObject ObstacleCheck(GameObject segment, Vector3 direction, LayerMask mask)
    {
        RaycastHit hit;
        Physics.Raycast(segment.transform.position + new Vector3(0, 0.5f, 0), direction, out hit, 1f, mask);
        if (hit.collider == null) return null;
        return hit.collider.gameObject;
    }

    private void FinishGame(bool isWin)
    {
        _canMove = false;
        //_levelControl.EndLevelUI(isWin);
    }
}