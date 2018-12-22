using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudPlayer : MonoBehaviour
{

    public Text healthText;
    public Text ammoText;
    public Image weaponImage;   
    public IntVariable weaponIndex;

    public FloatReference HP;
    public PlayerWeaponsInventory playerWeapons;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "" + HP.Value;

        if (weaponIndex.Value == 0)
        {
            ammoText.text = "many";
            weaponImage.sprite = playerWeapons.weapons[weaponIndex.Value].GetComponent<Bullet>().weaponSprite;
        }
        else
        {
            ammoText.text = "" + playerWeapons.ammo[weaponIndex.Value].Value;
            weaponImage.sprite = playerWeapons.weapons[weaponIndex.Value].GetComponent<Bullet>().weaponSprite;
        }
    }

}
