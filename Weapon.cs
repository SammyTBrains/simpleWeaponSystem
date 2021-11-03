using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private int _ammoTotal, _cooldownTime;

    public int AmmoTotal
    {
        get
        {
            return _ammoTotal;
        }
    }

    private int _ammoVal;
    public int AmmoVal
    {
        get
        {
            return _ammoVal;
        }
    }

    private bool _isCooldown;

    private void Start()
    {
        _ammoVal = _ammoTotal;
    }

    private void Update()
    {
        if (_isCooldown && _ammoVal <= 0)
        {
            _ammoVal = _ammoTotal;
        }
    }

    public IEnumerator ReduceAmmoRoutine(int weaponVal)
    {
        if (_ammoVal > 0)
        {
            _ammoVal--;
            UIManager2.Instance.SetAmmo(_ammoVal, weaponVal);
            yield return new WaitForSeconds(1);
        }
        else
        {
            _isCooldown = false;
            UIManager2.Instance.Reloading.SetActive(true);
            yield return new WaitForSeconds(_cooldownTime);
            UIManager2.Instance.Reloading.SetActive(false);
            _isCooldown = true;
        }
    }
}
