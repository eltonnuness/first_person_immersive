using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    private int currentTask = 0;
    [SerializeField] private TaskSO[] taskList;

    [SerializeField] private Terminal terminal;
    [SerializeField] private WeigthSensor weigthSensor;

    void Start()
    {
    }

    void Update()
    {
    }

    private void ReceivedTask(int taskNumber)
    {
        TaskSO task = taskList[taskNumber - 1];
        terminal.WriteOnScreen($"New Task {task.totalWeigthMin}/{task.totalWeigthMax}");
    }

    public void VerifyTask()
    {
        TaskSO task = taskList[currentTask - 1];
        int total = weigthSensor.GetTotal();

        if (total >= task.totalWeigthMin && total <= task.totalWeigthMax)
        {
            weigthSensor.Delivery();
        }

    }

    public void NextTask()
    {
        // Verify if can change task
        currentTask++;
        ReceivedTask(currentTask);
    }

}
