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

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    private void Start()
    {
        int currentNumberOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        equipments = new Equipment[currentNumberOfSlots];
        inventory = Inventory.instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }

    public void Equip(Equipment equipment)
    {
        int equipmentSlot = (int)equipment.equipmentSlot;
        Equipment currentEqupment = equipments[equipmentSlot];

        if (currentEqupment != null)
        {
            inventory.Add(currentEqupment);
        }

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(equipment, currentEqupment);
        }

        equipments[equipmentSlot] = equipment;
    }

    public void Unequip(int equipmentSlot)
    {
        Equipment currentEquipment = equipments[equipmentSlot];

        if (currentEquipment != null)
        {
            inventory.Add(currentEquipment);
        }

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(null, currentEquipment);
        }

        equipments[equipmentSlot] = null;
    }

    public void UnequipAll()
    {
        for (int i = 0; i < equipments.Length; i++)
        {
            Unequip(i);
        }
    }
}