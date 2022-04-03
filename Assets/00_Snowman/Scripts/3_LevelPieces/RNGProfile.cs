using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RNGProfile : MonoBehaviour
{
    protected int CumulativeChance;

    protected Dictionary<int, GameObject> allItems;

    protected BinarySearchTree Items;
    public void AddItem(GameObject item, int Chance)
    {
        if (allItems == null)
        {
            allItems = new Dictionary<int, GameObject>();
        }
        CumulativeChance += Chance;
        allItems.Add(CumulativeChance, item);

        if (Items == null)
        {
            Items = new BinarySearchTree();
            Items.Add(-1);
        }
        Items.Add(CumulativeChance);
    }

    public GameObject RetrieveRandomItem()
    {
        var rng = Random.Range(0, CumulativeChance);
        var index = Items.Find(rng);
        return allItems[index];
    }
}
