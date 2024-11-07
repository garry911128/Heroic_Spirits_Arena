using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public partial class BattleSceneUIManager
{

    public void PlayCharacterAnimation(int playerIndex, CharacterAction action)
    {
        StopAllCoroutines();  // ����ثe���b���񪺰ʵe
        StartCoroutine(characterAnimators[playerIndex].PlayAnimationCoroutine(action));
    }

}
