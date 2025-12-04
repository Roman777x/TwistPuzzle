using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] GameObject _bodyPrefab;
    public List<GameObject> _snakeSegments = new List<GameObject>();
    bool _canMove = true;

    private void Start()
    {
        transform.forward = new Vector3(0, 0, 1);
        _snakeSegments.Add(gameObject);
        AddBody();
    }

    private void Update()
    {
        if (_canMove)
        {
            switch (true)
            {
                case true when Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) && transform.forward != new Vector3(0, 0, -1):
                    Move(new Vector3(1,0,0), new Vector3(0,0,1));
                    break;

                case true when (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && transform.forward != new Vector3(1, 0, 0):
                    Move(new Vector3(0, 0, 1), new Vector3(-1, 0, 0));
                    break;

                case true when (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && transform.forward != new Vector3(0, 0, 1):
                    Move(new Vector3(-1, 0, 0), new Vector3(0, 0, -1));
                    break;


                case true when (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) && transform.forward != new Vector3(-1, 0, 0)):
                    Move(new Vector3(0, 0, -1), new Vector3(1, 0, 0));
                    break;
               
                case true when Input.GetKeyDown(KeyCode.Z):
                    AddBody();
                    break;
            }
        }
    }

    private void Move(Vector3 moveTo, Vector3 rotateTo)
    {
        Vector3 _pastPosition = transform.localPosition;
        Quaternion _pastRotation = transform.localRotation;
        Vector3 _temporaryPos;
        Quaternion _temporaryRot;
        
        foreach (GameObject segment in _snakeSegments)
        {
            if (segment.name == "Head")
            {
                segment.transform.position += moveTo;
                segment.transform.forward = rotateTo;
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
        }
    }
    private void AddBody()
    {
        GameObject _last = _snakeSegments[_snakeSegments.Count - 1];
        Vector3 _newPosition = _last.transform.localPosition - new Vector3(_last.transform.forward.z, 0, -_last.transform.forward.x);
        GameObject _newBody = Instantiate(_bodyPrefab, _newPosition, _last.transform.localRotation, transform.parent);
        _snakeSegments.Add(_newBody);
    }


}
    