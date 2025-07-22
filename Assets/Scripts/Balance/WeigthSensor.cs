using System.Collections.Generic;
using UnityEngine;

public class WeigthSensor : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text text;
    private Dictionary<int, Piece> items = new Dictionary<int, Piece>();

    int total = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int calculateTotal = 0;
        foreach (var item in items)
        {
            calculateTotal += item.Value.GetWeight();
        }
        total = calculateTotal;
        text.SetText(total.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        Piece piece;
        if (other.gameObject.TryGetComponent<Piece>(out piece))
        {
            items.TryAdd(piece.gameObject.GetInstanceID(), piece);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Piece piece;
        if (other.gameObject.TryGetComponent<Piece>(out piece))
        {
            items.Remove(piece.gameObject.GetInstanceID());
        }
    }

    public Dictionary<int, Piece> GetItems() => items;
    public int GetTotal() => total;

    public void Delivery()
    {
        foreach (var item in items)
        {
            Destroy(item.Value.gameObject);
        }
    }

}
