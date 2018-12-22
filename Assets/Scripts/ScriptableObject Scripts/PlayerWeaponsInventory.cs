using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerWeaponsInventory : ScriptableObject
{
    public List<GameObject> weapons = new List<GameObject>(); // a list of weapons the player has. This is a list so it doesn't matter when we obtain the weapon.
    public List<FloatVariable> ammo = new List<FloatVariable>(); // this coincides with the weapons as each will have its own ammo type as well. 
    public GameObject startWeapon; // The start weapon.
    public FloatVariable startWeaponAmmo; // The starting ammo.

    // This removes everything from the list and adds back the main weapon and ammo type. 
    public void ResetWeapons()
    {
        weapons.Clear();
        weapons.Add(startWeapon);
        ammo.Clear();
        ammo.Add(startWeaponAmmo);
    }

}
