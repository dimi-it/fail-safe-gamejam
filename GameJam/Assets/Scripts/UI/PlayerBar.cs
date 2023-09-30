using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : MonoBehaviour
{
    InGameUI inGameUi;
    public Image hpImage;
    public Image roleImage;

    int hp = 3;
    Role role = Role.Mecha; 
    private void Start()
    {
        inGameUi = FindAnyObjectByType<InGameUI>();
    }
    public void UpdateHp(int hp)
    {
        hpImage.sprite = inGameUi.BarImages[hp];
        if (hp == 0)
        {
            UpdateRole(Role.Dead);
        }
    }
    public void UpdateRole(Role role)
    {
        roleImage.sprite = inGameUi.RoleImages[(int)role];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Role cRole = (Role)((int)role + 1);
            role = cRole;
            UpdateRole(cRole);
            UpdateHp(--hp);
        }
    }
    public enum Role
    {
        Mecha,
        Ninja,
        Pirate,
        Cowboy,
        Dead
    }
}
