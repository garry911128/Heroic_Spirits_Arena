using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAnimator : MonoBehaviour
{
    public Image characterImage; // 用來顯示角色圖片的 Image 元件
    public Sprite[] idleSprites; // 閒置狀態的圖片序列
    public Sprite[] attackSprites; // 攻擊狀態的圖片序列
    public Sprite[] defendSprites; // 防禦狀態的圖片序列

    private Sprite[] currentActionSprites; // 當前狀態的圖片序列

    public void LoadSprite(Character character)
    {
        characterImage.sprite = character.idleSprites[0];
        idleSprites = character.idleSprites;
        attackSprites = character.attackSprites;
        defendSprites = character.defendSprites;
        currentActionSprites = idleSprites;
    }

    public IEnumerator PlayAnimationCoroutine(CharacterAction action)
    {
        float frameDuration = 0.1f;  // 每幀持續時間，可自行調整

        switch (action)
        {
            case CharacterAction.ATTACK:
                currentActionSprites = attackSprites; // 攻擊動畫
                break;
            case CharacterAction.DEFENSE:
                currentActionSprites = defendSprites; // 防禦動畫
                break;
            case CharacterAction.IDLE:
            default:
                currentActionSprites = idleSprites; // 待機動畫
                break;
        }

        for (int i = 0; i < currentActionSprites.Length; i++)
        {
            characterImage.sprite = currentActionSprites[i];
            yield return new WaitForSeconds(frameDuration);
        }

        currentActionSprites = idleSprites; // 播放完畢後切換回待機狀態
        characterImage.sprite = currentActionSprites[0];
    }
}
