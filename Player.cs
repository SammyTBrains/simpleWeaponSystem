using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PrimaryGuns
    {
        SHOTGUN,
        VECTOR,
        RPD,
        AK_47,
        ACR
    }

    public enum SecondaryGuns
    {
        XM25,
        DESERT_EAGLE,
        FMG9,
        SKORPION,
        G18
    }

    [System.Serializable]
    public struct PrimaryGun
    {
        public PrimaryGuns primaryGun;
        public GameObject prefab;
        public Sprite weaponPicture;
    }

    [System.Serializable]
    public struct SecondaryGun
    {
        public SecondaryGuns secondaryGun;
        public GameObject prefab;
        public Sprite weaponPicture;
    }

    [SerializeField]
    private PrimaryGun[] _primaryGuns;
    [SerializeField]
    private SecondaryGun[] _secondaryGuns;

    private GameObject _primaryWeapon1, _primaryWeapon2;
    private GameObject _secondaryWeapon;

    private GameObject weapon1, weapon2, weapon3;

    private Vector3 pos1, pos2, pos3;
    private int _primaryCount;
    private int _secondaryCount;

    // Start is called before the first frame update
    void Start()
    {
        _primaryWeapon1 = _primaryGuns[0].prefab;
        UIManager2.Instance.SetPrimaryWeapon1Sprite(_primaryGuns[0].weaponPicture);
        _primaryWeapon2 = _primaryGuns[1].prefab;
        UIManager2.Instance.SetPrimaryWeapon2Sprite(_primaryGuns[1].weaponPicture);
        _primaryCount = 1;
        _secondaryWeapon = _secondaryGuns[0].prefab;
        UIManager2.Instance.SetSecondaryWeaponSprite(_primaryGuns[0].weaponPicture);

        UIManager2.Instance.AmmoTotalText1 = _primaryWeapon1.GetComponent<Weapon>().AmmoTotal.ToString();
        UIManager2.Instance.AmmoTotalText2 = _primaryWeapon2.GetComponent<Weapon>().AmmoTotal.ToString();
        UIManager2.Instance.AmmoTotalText3 = _secondaryWeapon.GetComponent<Weapon>().AmmoTotal.ToString();

        UIManager2.Instance.SetAmmo(_primaryWeapon1.GetComponent<Weapon>().AmmoVal, 1);
        UIManager2.Instance.SetAmmo(_primaryWeapon2.GetComponent<Weapon>().AmmoVal, 2);
        UIManager2.Instance.SetAmmo(_secondaryWeapon.GetComponent<Weapon>().AmmoVal, 3);

        pos1 = transform.position;
        pos1.x += 5;
        pos2 = transform.position;
        pos2.x -= 5;
        pos3 = transform.position;
        pos3.y -= 2;

        weapon1 = Instantiate(_primaryWeapon1, pos1, _primaryWeapon1.transform.rotation);
        weapon2 = Instantiate(_primaryWeapon2, pos2, _primaryWeapon2.transform.rotation);
        weapon3 = Instantiate(_secondaryWeapon, pos3, _secondaryWeapon.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SwitchPrimayWeapon();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SwitchSecondaryWeapon();
        }

        FireWeapon();
    }

    private void SwitchPrimayWeapon()
    {
        Destroy(weapon1);
        Destroy(weapon2);

        if (_primaryCount >= _primaryGuns.Length - 1)
        {
            _primaryCount = 0;
        }
        else
        {
            _primaryCount++;
        }

        int randNum = Random.Range(1, 3);

        switch (randNum)
        {
            case 1:
                _primaryWeapon1 = _primaryGuns[_primaryCount].prefab;
                UIManager2.Instance.AmmoTotalText1 = _primaryWeapon1.GetComponent<Weapon>().AmmoTotal.ToString();
                UIManager2.Instance.SetPrimaryWeapon1Sprite(_primaryGuns[_primaryCount].weaponPicture);
                break;
            case 2:
                _primaryWeapon2 = _primaryGuns[_primaryCount].prefab;
                UIManager2.Instance.AmmoTotalText2 = _primaryWeapon2.GetComponent<Weapon>().AmmoTotal.ToString();
                UIManager2.Instance.SetPrimaryWeapon2Sprite(_primaryGuns[_primaryCount].weaponPicture);
                break;
        }

        if (_primaryWeapon1 == _primaryWeapon2)
        {
            SwitchPrimayWeapon();
        }
        else
        {
            weapon1 = Instantiate(_primaryWeapon1, pos1, _primaryWeapon1.transform.rotation);
            weapon2 = Instantiate(_primaryWeapon2, pos2, _primaryWeapon2.transform.rotation);

            UIManager2.Instance.SetAmmo(_primaryWeapon1.GetComponent<Weapon>().AmmoVal, 1);
            UIManager2.Instance.SetAmmo(_primaryWeapon2.GetComponent<Weapon>().AmmoVal, 2);
        }
    }

    private void SwitchSecondaryWeapon()
    {
        Destroy(weapon3);

        if (_secondaryCount >= _secondaryGuns.Length - 1)
        {
            _secondaryCount = 0;
        }
        else
        {
            _secondaryCount++;
        }

        _secondaryWeapon = _secondaryGuns[_secondaryCount].prefab;
        UIManager2.Instance.AmmoTotalText3 = _secondaryWeapon.GetComponent<Weapon>().AmmoTotal.ToString();
        UIManager2.Instance.SetSecondaryWeaponSprite(_primaryGuns[_secondaryCount].weaponPicture);

        weapon3 = Instantiate(_secondaryWeapon, pos3, _secondaryWeapon.transform.rotation);

        UIManager2.Instance.SetAmmo(_secondaryWeapon.GetComponent<Weapon>().AmmoVal, 3);
    }

    private void FireWeapon()
    {
        
        if (Input.GetMouseButton(0))
        {
            Damage(_primaryWeapon1, 1);
        }
        else if (Input.GetMouseButton(1))
        {
            Damage(_primaryWeapon2, 2);
        }
        else if (Input.GetKey(KeyCode.F))
        {
            Damage(_secondaryWeapon, 3);
        }
    }

    private void Damage(GameObject weapon, int weaponVal)
    {
        RaycastHit hit;
        if (Physics.Raycast(weapon.transform.position, Vector3.forward, out hit))
        {
            StartCoroutine(weapon.GetComponent<Weapon>().ReduceAmmoRoutine(weaponVal));
            //Damage Enemy and Create Effects
        }
    }
}
