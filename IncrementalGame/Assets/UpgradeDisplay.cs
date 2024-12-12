using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeDisplay : MonoBehaviour
{
    public new TMP_Text name;
    public TMP_Text amount;
    public TMP_Text strength;
    public TMP_Text cost;

    public Upgrade upgrade;

    void Update()
    {
        name.text = upgrade.name;
        amount.text = "Owned: " + string.Format("{0:0.0}", upgrade.amount);
        cost.text = "Cost: " + string.Format("{0:0.0}", upgrade.cost);
        strength.text = "Strength: " + string.Format("{0:0.0}", upgrade.strength);
    }
}
