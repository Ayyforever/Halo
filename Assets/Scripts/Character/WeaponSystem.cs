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

        // ��ⰴ���������л�����
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

        // �������ּ����л�����
        for (int i = 0; i < transform.childCount; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                selectedWeapon = i;
        }

        // ����л��������������ѡ�������ķ���
        if (previousSelectedWeapon != selectedWeapon)
            SelectWeapon();
    }

    private void SelectWeapon()
    {
        // ��������������������/���ö�Ӧ����Ϸ����
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

