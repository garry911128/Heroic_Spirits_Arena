using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public partial class BattleSceneUIManager
{

    public void PlayCharacterAnimation(int playerIndex, CharacterAction action)
    {
        StopAllCoroutines();  // ����ثe���b���񪺰ʵe
        //StartCoroutine(PlayAnimationCoroutine(characterAnimators[playerIndex].characterImage, action, GameManager.instance.players[playerIndex]));
        characterAnimators[playerIndex].UpdateAnimation(action, 0.1f);
    }

    //private IEnumerator PlayAnimationCoroutine(Image characterImage, CharacterAction action, Character character)
    //{
    //    Sprite[] animationFrames;
    //    float frameDuration = 0.1f;  // �C�V����ɶ��A�i�ۦ�վ�

    //    switch (action)
    //    {
    //        case CharacterAction.ATTACK:
    //            animationFrames = character.attackSprites; // �����ʵe
    //            break;
    //        case CharacterAction.DEFENSE:
    //            animationFrames = character.defendSprites; // ���m�ʵe
    //            break;
    //        case CharacterAction.IDLE:
    //        default:
    //            animationFrames = character.idleSprites; // �ݾ��ʵe
    //            break;
    //    }

    //    for (int i = 0; i < animationFrames.Length; i++)
    //    {
    //        characterImage.sprite = animationFrames[i];
    //        yield return new WaitForSeconds(frameDuration);
    //    }

    //}
}
