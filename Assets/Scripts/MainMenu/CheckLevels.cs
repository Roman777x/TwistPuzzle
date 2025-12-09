using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CheckLevels : MonoBehaviour
{
    [SerializeField] List<GameObject> Levels;
    public void Check()
    {
        bool FirstBlockLevel = false;
        foreach (var level in Levels)
        {

            if (GameData.CompliteForestLevels[gameObject.transform.parent.name + level.name])
            {
                level.transform.Find("Lock").gameObject.SetActive(false);
                
            }
            else if (!FirstBlockLevel)
            {
                FirstBlockLevel = true;
                level.transform.Find("Lock").gameObject.SetActive(false);

            }
            else
            {
                level.transform.Find("Lock").gameObject.SetActive(true);

            }
        }
    }
}
