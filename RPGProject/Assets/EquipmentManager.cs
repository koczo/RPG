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

    public Equipment[] defaultEquipments;
    Equipment[] equipments;
    SkinnedMeshRenderer[] meshRenderers;
    Inventory inventory;

    public SkinnedMeshRenderer targetSkinnedMesh;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    private void Start()
    {
        inventory = Inventory.instance;

        int currentNumberOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;

        equipments = new Equipment[currentNumberOfSlots];
        meshRenderers = new SkinnedMeshRenderer[currentNumberOfSlots];

        EquipDefaultItems();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }

    public void Equip(Equipment newEquipment)
    {
        int equipmentSlot = (int)newEquipment.equipmentSlot;
        Equipment oldEquipment = Unequip(equipmentSlot);

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newEquipment, oldEquipment);
        }

        SetEquipmentBlendShapes(newEquipment, 100);

        equipments[equipmentSlot] = newEquipment;

        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newEquipment.meshRendere);
        newMesh.transform.parent = targetSkinnedMesh.transform;

        newMesh.bones = targetSkinnedMesh.bones;
        newMesh.rootBone = targetSkinnedMesh.rootBone;

        meshRenderers[equipmentSlot] = newMesh;
    }

    public Equipment Unequip(int equipmentSlot)
    {
        Equipment currentEquipment = equipments[equipmentSlot];

        if (currentEquipment != null)
        {
            if (meshRenderers[equipmentSlot] != null)
            {
                Destroy(meshRenderers[equipmentSlot].gameObject);
            }

            SetEquipmentBlendShapes(currentEquipment, 0);

            inventory.Add(currentEquipment);

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, currentEquipment);
            }

            equipments[equipmentSlot] = null;
        }

        return currentEquipment;
    }

    public void UnequipAll()
    {
        for (int i = 0; i < equipments.Length; i++)
        {
            Unequip(i);
        }

        EquipDefaultItems();
    }

    private void SetEquipmentBlendShapes(Equipment item, int weight)
    {
        foreach (EquipmentMeshRegion blendShape in item.coverdMeshRegions)
        {
            targetSkinnedMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    private void EquipDefaultItems()
    {
        foreach (Equipment item in defaultEquipments)
        {
            Equip(item);
        }
    }
}