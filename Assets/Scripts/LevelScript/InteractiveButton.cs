using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveButton : MonoBehaviour
{
    [SerializeField] List<GameObject> _connectBox = new List<GameObject>();
    private bool _canActve = true;
    [SerializeField] bool _canActveSeveralTimes = false;

    public void Action()
    {
        if (_canActve || _canActveSeveralTimes)
        {
            foreach (GameObject box in _connectBox)
            {
                if (box) box.SetActive(!box.activeSelf);
            }
        _canActve = false;
        }
    }
        
}
