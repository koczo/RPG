using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {

    private void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifire(newItem.armorModifier);
            damage.AddModifire(newItem.damageModifire);
        }

        if (oldItem != null)
        {
            armor.RemoveModifire(oldItem.armorModifier);
            damage.RemoveModifire(oldItem.damageModifire);
        }
    }

    public override void Die()
    {
        base.Die();
        // Gameover
        Debug.Log("Game over!!!");
    }
}
