using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : Item
{
    [Range(0, Character.MAX_LEVEL)]
    public int LevelRequirement;

    public int StrengthBonus;
    public int DexterityBonus;
    public int IntelligenceBonus;
    public int ConstitutionBonus;

    public bool overRideNumRNGRolls;
    public int NumRNGRolls;

    public bool overRideRarityModifier;
    public float rarityModifier;

    public void GenerateRandomGear()
    {
        StrengthBonus = 0;
        DexterityBonus = 0;
        IntelligenceBonus = 0;
        ConstitutionBonus = 0;

        if (!overRideRarityModifier)
        {
            switch (Rarity)
            {
                case Rarity.COMMON:
                    rarityModifier = 1.2f;
                    break;

                case Rarity.UNCOMMON:
                    rarityModifier = 1.5f;
                    break;

                case Rarity.RARE:
                    rarityModifier = 1.8f;
                    break;

                case Rarity.EPIC:
                    rarityModifier = 2.5f;
                    break;

                case Rarity.LEGENDARY:
                    rarityModifier = 3.5f;
                    break;
            }
        }
        if(!overRideNumRNGRolls)
        {
            switch (Rarity)
            {
                case Rarity.COMMON:
                    NumRNGRolls = 1;
                    break;

                case Rarity.UNCOMMON:
                    NumRNGRolls = 2;
                    break;

                case Rarity.RARE:
                    NumRNGRolls = 2;
                    break;

                case Rarity.EPIC:
                    NumRNGRolls = 3;
                    break;

                case Rarity.LEGENDARY:
                    NumRNGRolls = 3;
                    break;
            }
        }
        for(int i = 0; i < NumRNGRolls; i++)
        {
            int randNum = Random.Range(0, 4);

            switch(randNum)
            {
                case 0:
                    StrengthBonus += (int)(Random.Range(LevelRequirement, (float)LevelRequirement * 10) * rarityModifier);
                    break;
                case 1:
                    DexterityBonus += (int)(Random.Range(LevelRequirement, (float)LevelRequirement * 10) * rarityModifier);
                    break;
                case 2:
                    IntelligenceBonus += (int)(Random.Range(LevelRequirement, (float)LevelRequirement * 10) * rarityModifier);
                    break;
                case 3:
                    ConstitutionBonus += (int)(Random.Range(LevelRequirement, (float)LevelRequirement * 10) * rarityModifier);
                    break;
            }
        }
    }
}
