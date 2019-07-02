using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Upgrade { NONE, BOOTS}

public class PlayerUpgrades : MonoBehaviour
{
    Upgrade currentUpgrade = Upgrade.NONE;
    ItemDisplay itemDisplay;

    private void Start()
    {
        itemDisplay = FindObjectOfType<ItemDisplay>();
    }

    public void UpgradeTo(Upgrade u)
    {
        currentUpgrade = u;
        itemDisplay.SetItem(currentUpgrade);
    }

    public Upgrade GetCurrentUpgrade()
    {
        return currentUpgrade;
    }
}
