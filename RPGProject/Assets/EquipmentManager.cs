using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    #region Singleton

    public static EquipmentManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    #endregion

    Equipment[] equipments;
    Inventory inventory;

    private void Start()
    {
        int currentNumberOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        equipments = new Equipment[currentNumberOfSlots];
        inventory = Inventory.instance;
    }

    public void Equip(Equipment equipment)
    {
        int equipmentSlot = (int)equipment.equipmentSlot;
        Equipment currentEqupment = equipments[equipmentSlot];

        if (currentEqupment != null)
        {
            inventory.Add(currentEqupment);
        }

        equipments[equipmentSlot] = equipment;
    }
}
