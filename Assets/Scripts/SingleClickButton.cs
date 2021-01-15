using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public abstract class SingleClickButton : Button
{
    private bool alreadyActivated;
    protected override void HandleMultipleClicks()
    {
        if (alreadyActivated)
            return;

        alreadyActivated = true;

        base.HandleMultipleClicks();
    }
}
