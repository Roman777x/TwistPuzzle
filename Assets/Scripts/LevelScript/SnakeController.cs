using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SnakeController : MonoBehaviour
{
    LevelControl _levelControl;
    [SerializeField] GameObject _bodyPrefab;
    public List<GameObject> snakeSegments = new List<GameObject>();
    public List<int> isSnakeSegmentsFooting = new List<int>();
    public bool canMove = true;
    private bool isFalling = false;
    private bool isDashing = false;
    private int countFalls = 0;
    private int maxCountFalls = 3;
    [SerializeField] LayerMask Blocks;
    [SerializeField] LayerMask InteractiveObjects;
    [SerializeField] int numberOfBody;

    private void Start()
    {
        _levelControl = FindAnyObjectByType<LevelControl>();
        snakeSegments.Add(gameObject);
        for (int i = 1; i <= numberOfBody; i++) AddBody();
    }

    private void Update()
    {
        if (canMove && !_levelControl.isFinish && !isFalling && !isDashing)
        {
            switch (true)
            {
                case true when (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && (transform.forward != Vector3.back || snakeSegments.Count == 1):
                    Action(Vector3.forward, true);
                    break;

                case true when (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && (transform.forward != Vector3.right || snakeSegments.Count == 1):
                    Action(Vector3.left, true);
                    break;

                case true when (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && (transform.forward != Vector3.forward || snakeSegments.Count == 1):
                    Action(Vector3.back, true);
                    break;

                case true when (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && (transform.forward != Vector3.left || snakeSegments.Count == 1):
                    Action(Vector3.right, true);
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

    private void Action(Vector3 moveTo, bool isNeedAduit)
    {
        if (!isNeedAduit || !IsObstacle—heck(gameObject, moveTo))
        {
            GameObject _interactObject = ObstacleCheck(gameObject, moveTo, InteractiveObjects);

            Move(moveTo);
            ActionInteractiveObject(_interactObject);
            IsFall();
        }
    }

    private void Move(Vector3 moveTo)
    {
        Vector3 _pastPosition = transform.localPosition;
        Quaternion _pastRotation = transform.localRotation;
        Vector3 _temporaryPos;
        Quaternion _temporaryRot;

        foreach (GameObject segment in snakeSegments)
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

        }
    }

    private void IsFall()
    {
        foreach (GameObject segment in snakeSegments)
        {
            if (IsObstacle—heck(segment, Vector3.down)) isSnakeSegmentsFooting.Add(1);
            else isSnakeSegmentsFooting.Add(0);
            isFalling = false;
        }
        if (isSnakeSegmentsFooting.Sum() == 0)
        {
            isFalling = true;
            StartCoroutine(FallAnimation());
        }
        else countFalls = 0;
        isSnakeSegmentsFooting.Clear();
    }

    private IEnumerator FallAnimation()
    {
        yield return new WaitForSeconds(0.25f);
        GameObject _interactObject = ObstacleCheck(gameObject, Vector3.down, InteractiveObjects);
        foreach (GameObject segment in snakeSegments)
        {
            segment.transform.position += Vector3.down;
        }
        countFalls++;
        if (countFalls == maxCountFalls) FinishGame(false);
        ActionInteractiveObject(_interactObject);
        IsFall();
    }
   

    private void ActionInteractiveObject(GameObject _interactObject)
    {
        if (_interactObject)
        {
            switch (true)
            {
                case true when _interactObject.CompareTag("Apple"):
                    Destroy(_interactObject);
                    AddBody();
                    break;

                case true when _interactObject.CompareTag("Bomb"):
                    Destroy(_interactObject);
                    RemoveBody();
                    break;

                case true when _interactObject.CompareTag("Booster") && _interactObject.transform.forward != -gameObject.transform.forward:
                    StartCoroutine(Dash(_interactObject));
                    break;

                case true when _interactObject.CompareTag("Finish"):
                    FinishGame(true);
                    break;
                
                case true when _interactObject.CompareTag("Spike"):
                    FinishGame(false);
                    break;
                
                case true when _interactObject.CompareTag("Button"):
                    _interactObject.GetComponent<InteractiveButton>().Action();
                    break;

            }
        }
    }

    private void AddBody()
    {
        GameObject _last = snakeSegments[snakeSegments.Count - 1];
        Vector3 _newPosition = _last.transform.localPosition - _last.transform.forward;
        GameObject _newBody = Instantiate(_bodyPrefab, _newPosition, _last.transform.localRotation, transform.parent);
        snakeSegments.Add(_newBody);
    }

    private void RemoveBody()
    {
        GameObject _last = snakeSegments[snakeSegments.Count - 1];
        snakeSegments.RemoveAt(snakeSegments.Count - 1);
        Destroy(_last);
    }

    private IEnumerator Dash(GameObject Booster)
    {
        isDashing = true;
        while (!IsObstacle—heck(gameObject, Booster.transform.forward) && canMove)
        {
            if (_levelControl.isFinish) break;
            yield return new WaitForSeconds(0.1f);
            GameObject _interactObject = ObstacleCheck(gameObject, Booster.transform.forward, InteractiveObjects);

            Move(Booster.transform.forward);
            ActionInteractiveObject(_interactObject);
            if (_interactObject && _interactObject.CompareTag("Booster")) break;
        }
        isDashing = false;
        IsFall();
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
        canMove = false;
        _levelControl.isFinish = isWin;
        _levelControl.ComplitLevel(isWin);
    }
}