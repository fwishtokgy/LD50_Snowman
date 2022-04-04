using System.Collections.Generic;
using UnityEngine;

public class RNGProfile
{
    protected int CumulativeChance;

    protected Dictionary<int, GameObject> allItems;

    protected BinarySearchTree Items;

    protected int NoDropChance;

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
    public void SetChanceOfNoDrop(int chance)
    {
        NoDropChance = chance;
    }
    public void SetChanceOfDrop(float chance)
    {
        NoDropChance = 200;
    }
    public void SetChanceOfDrop(int chance)
    {
        NoDropChance = chance;
    }


    public GameObject RetrieveRandomItem()
    {
        if (Items == null) return null;
        //Debug.Log("Attempting Retrieval within "+ allItems.Count);
        var fullChance = NoDropChance + CumulativeChance;
        var rng = Random.Range(0, fullChance);
        if (rng <= CumulativeChance)
        {
            var index = Items.Find(rng);
            return allItems[index];
        }
        return null;
    }
}
