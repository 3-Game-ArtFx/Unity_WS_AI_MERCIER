using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinSysManager : MonoBehaviour
{
    int playerAmount;

    [SerializeField] private List<Transform> _targets;

    // Start is called before the first frame update
    void Start()
    {
        playerAmount += GameObject.FindGameObjectsWithTag("Team1").Length;
        playerAmount += GameObject.FindGameObjectsWithTag("Team2").Length;
        Debug.Log("There are " + playerAmount + " in the current game");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Transform> GetTargets()
    {
        return _targets;
    }

    public void UpdateTeam()
    {
        if(GameObject.FindGameObjectsWithTag("Team1").Length == playerAmount)
        {
            Debug.Log("Team 1 Won");
        }
        else if(GameObject.FindGameObjectsWithTag("Team2").Length == playerAmount)
        {
            Debug.Log("Team 2 Won");
        }
    }
}
