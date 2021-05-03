using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour
{
    public Ability abilityInSlot;
    public Image abilityImage;

    private void Start()
    {
        abilityImage.sprite = abilityInSlot.abilityIcon;
    }

    private void Update()
    {
        setAbililtyCooldownPicture();
    }

    private void setAbililtyCooldownPicture()
    {
        if(abilityInSlot.abilityOnCooldown)
        {
            abilityImage.color = Color.gray;
        }
        else
        {
            abilityImage.color = Color.white;
        }
    }
}
