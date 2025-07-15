using UnityEngine;

public class Piece : MonoBehaviour
{
    private int weight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weight = Random.Range(30, 48);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetWeight() => weight;
}
