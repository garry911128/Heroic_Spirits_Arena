using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionSelector
{
    void ShowActionSelector(Character currentAttacker, System.Action<CharacterAction> onActionSelected);
}

