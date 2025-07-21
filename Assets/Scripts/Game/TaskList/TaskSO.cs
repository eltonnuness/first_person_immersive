using System;
using UnityEditor.Recorder.Input;
using UnityEngine;

[CreateAssetMenu(fileName = "Task", menuName = "Tasks/New Task")]
public class TaskSO : ScriptableObject
{
    public string id;
    public Piece[] objects;
    public int totalWeigthMin;
    public int totalWeigthMax;

    public int CalculateWeigth()
    {
        int total = 0;
        for (int i = 0; i < objects.Length; i++)
        {
            total += objects[i].GetWeight();
        }
        return total;
    }
}
