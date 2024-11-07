using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public partial class BattleSceneUIManager
{

    public void PlayCharacterAnimation(int playerIndex, CharacterAction action)
    {
        StopAllCoroutines();  // 停止目前正在撥放的動畫
        //StartCoroutine(PlayAnimationCoroutine(characterAnimators[playerIndex].characterImage, action, GameManager.instance.players[playerIndex]));
        characterAnimators[playerIndex].UpdateAnimation(action, 0.1f);
    }

    //private IEnumerator PlayAnimationCoroutine(Image characterImage, CharacterAction action, Character character)
    //{
    //    Sprite[] animationFrames;
    //    float frameDuration = 0.1f;  // 每幀持續時間，可自行調整

    //    switch (action)
    //    {
    //        case CharacterAction.ATTACK:
    //            animationFrames = character.attackSprites; // 攻擊動畫
    //            break;
    //        case CharacterAction.DEFENSE:
    //            animationFrames = character.defendSprites; // 防禦動畫
    //            break;
    //        case CharacterAction.IDLE:
    //        default:
    //            animationFrames = character.idleSprites; // 待機動畫
    //            break;
    //    }

    //    for (int i = 0; i < animationFrames.Length; i++)
    //    {
    //        characterImage.sprite = animationFrames[i];
    //        yield return new WaitForSeconds(frameDuration);
    //    }

    //}
}
