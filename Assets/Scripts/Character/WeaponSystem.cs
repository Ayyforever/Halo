using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public int selectedWeapon = 0;

    private void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        // 检测按键输入来切换武器
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        // 按下数字键来切换武器
        for (int i = 0; i < transform.childCount; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                selectedWeapon = i;
        }

        // 如果切换了武器，则调用选择武器的方法
        if (previousSelectedWeapon != selectedWeapon)
            SelectWeapon();
    }

    private void SelectWeapon()
    {
        // 遍历所有武器，并激活/禁用对应的游戏对象
        int weaponIndex = 0;
        foreach (Transform weapon in transform)
        {
            if (weaponIndex == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);

            weaponIndex++;
        }
    }
}

