using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]

public class EquipmentObject : ItemObject
{
    public int attackBonus;
    public int magicBonus;
    public int defenseBonus;
    public float fireResist;
    public float wateResist;
    public float electricResist;
    public float poisionResist;
    public void Awake()
    {
        type = ItemType.Equipment;
    }
}
