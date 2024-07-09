using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Custom : TMP_Dropdown
{
    protected override GameObject CreateBlocker(Canvas rootCanvas)
    {
        var blocker =  base.CreateBlocker(rootCanvas);

        blocker.GetComponent<Canvas>().sortingOrder += 3;
        blocker.GetComponent<Canvas>().overrideSorting =false;
        return blocker;

    }
}
