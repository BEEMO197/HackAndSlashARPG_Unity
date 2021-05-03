using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{

    public GameObject descriptionsGameObject;

    public TMPro.TextMeshProUGUI StrengthValue;
    public TMPro.TextMeshProUGUI DexterityValue;
    public TMPro.TextMeshProUGUI ConstitutionValue;
    public TMPro.TextMeshProUGUI IntelligenceVValue;

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void updateDescriptions(Gear gear)
    {
        if(gear != null)
        {
            descriptionsGameObject.SetActive(true);

            StrengthValue.text = gear.StrengthBonus.ToString();
            DexterityValue.text = gear.DexterityBonus.ToString();
            ConstitutionValue.text = gear.ConstitutionBonus.ToString();
            IntelligenceVValue.text = gear.IntelligenceBonus.ToString();
        }
        else
        {
            descriptionsGameObject.SetActive(false);

            StrengthValue.text = "";
            DexterityValue.text = "";
            ConstitutionValue.text = "";
            IntelligenceVValue.text = "";
        }
    }
}
