using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject invetory;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEMS;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    void Start()
    {
        CreateDisplay();
    }

    private void CreateDisplay()
    {
        for(int i = 0; i < invetory.Container.Count; i++)
        {
            var obj = Instantiate(invetory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = invetory.Container[i].amount.ToString("n0");
            itemsDisplayed.Add(invetory.Container[i], obj);
        }
    }

    void Update()
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        for(int i =0; i < invetory.Container.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(invetory.Container[i]))
            {
                itemsDisplayed[invetory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = invetory.Container[i].amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(invetory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = invetory.Container[i].amount.ToString("n0");
                itemsDisplayed.Add(invetory.Container[i], obj);
            }
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START +(X_SPACE_BETWEEN_ITEMS * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
    }
}
