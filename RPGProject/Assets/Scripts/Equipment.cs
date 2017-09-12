using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipmentSlot;
    public EquipmentMeshRegion[] coverdMeshRegions;
    public SkinnedMeshRenderer meshRendere;

    public int armorModifier;
    public int damageModifire;

    public override void Use()
    {
        base.Use();

        // Equip item
        EquipmentManager.instance.Equip(this);

        // Remove item from inventory
        Inventory.instance.Remove(this);
    }
}

public enum EquipmentSlot
{
    Head,
    Chest,
    Legs,
    Weapon,
    Shield,
    Feet
}

// Corresponds to body blendshapes
public enum EquipmentMeshRegion
{
    Legs,
    Arms,
    Torso
}