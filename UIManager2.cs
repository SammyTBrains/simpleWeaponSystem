using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager2 : MonoBehaviour
{
    [SerializeField]
    private Image _primaryWeapon1Sprite, _primaryWeapon2Sprite, _secondaryWeaponSprite;
    [SerializeField]
    private Text _ammoTotalText1, _curAmmoText1, _ammoTotalText2, _curAmmoText2, _ammoTotalText3, _curAmmoText3;
    [SerializeField]
    private GameObject _reloading;

    public GameObject Reloading
    {
        get
        {
            return _reloading;
        }
    }

    public string AmmoTotalText1
    {
        set
        {
            _ammoTotalText1.text = value;
        }
    }

    public string AmmoTotalText2
    {
        set
        {
            _ammoTotalText2.text = value;
        }
    }

    public string AmmoTotalText3
    {
        set
        {
            _ammoTotalText3.text = value;
        }
    }

    private static UIManager2 _instance;
    public static UIManager2 Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is null!");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void SetAmmo(int ammo, int weapon)
    {
        switch (weapon)
        {
            case 1:
                _curAmmoText1.text = ammo.ToString();
                break;
            case 2:
                _curAmmoText2.text = ammo.ToString();
                break;
            case 3:
                _curAmmoText3.text = ammo.ToString();
                break;
        }
       
    }

    public void SetPrimaryWeapon1Sprite(Sprite sprite)
    {
        _primaryWeapon1Sprite.sprite = sprite;
    }

    public void SetPrimaryWeapon2Sprite(Sprite sprite)
    {
        _primaryWeapon2Sprite.sprite = sprite;
    }

    public void SetSecondaryWeaponSprite(Sprite sprite)
    {
        _secondaryWeaponSprite.sprite = sprite;
    }
}
