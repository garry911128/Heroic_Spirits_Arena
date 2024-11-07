using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public partial class BattleSceneUIManager
{

    public void PlayCharacterAnimation(int playerIndex, CharacterAction action)
    {
        StopAllCoroutines();  // 停止目前正在撥放的動畫
        StartCoroutine(characterAnimators[playerIndex].PlayAnimationCoroutine(action));
    }

}
