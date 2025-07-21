using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] private int MIN_WEIGHT = 30;
    [SerializeField] private int MAX_WEIGHT = 48;
    [SerializeField] private ItemColor color = ItemColor.NONE;

    //State
    private int weight;
    private ItemColor itemColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weight = Random.Range(MIN_WEIGHT, MAX_WEIGHT);
        itemColor = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetWeight() => weight;

    public ItemColor GetColor() => itemColor;

    public enum ItemColor
    {
        NONE, RED, BLUE, YELLOW
    }
}
