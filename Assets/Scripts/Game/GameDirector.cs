using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    private int currentTask = 0;
    [SerializeField] private TaskSO[] taskList;

    [SerializeField] private Terminal terminal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void ReceivedTask(int taskNumber)
    {
        TaskSO task = taskList[taskNumber - 1];
        terminal.WriteOnScreen($"You received a new task: Weigth Between MIN={task.totalWeigthMin} / MAX={task.totalWeigthMax}");
    }

    public void VerifyTask(Piece[] pieces)
    {
        
    }

    public void NextTask()
    {
        currentTask++;
        ReceivedTask(currentTask);
    }

}
