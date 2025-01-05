using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Objective
{

    public int id;
    public string description;
    public bool completed;

    public void Complete()
    {
        if (!completed)
        {
            completed = true;
        }
    }

}
