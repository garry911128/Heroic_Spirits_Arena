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
    private int currentFrame = 0; // 當前幀
    private bool isAnimating = false; // 是否正在播放動畫

    // 播放動畫
    public void PlayAnimation(Sprite[] sprites, float frameRate)
    {
        if (isAnimating)
            return; // 如果已經在播放動畫，則不再播放

        isAnimating = true;
        currentActionSprites = sprites;
        currentFrame = 0;
        StartCoroutine(PlayAnimationCoroutine(frameRate));
    }

    private IEnumerator PlayAnimationCoroutine(float frameRate)
    {
        while (currentFrame < currentActionSprites.Length)
        {
            characterImage.sprite = currentActionSprites[currentFrame];
            currentFrame++;
            yield return new WaitForSeconds(frameRate); // 根據幀率等待
        }

        isAnimating = false; // 動畫播放結束
        currentActionSprites = idleSprites;
        characterImage.sprite = currentActionSprites[0];
    }

    // 更新動畫根據角色的當前狀態
    public void UpdateAnimation(CharacterAction currentAction, float frameRate)
    {
        switch (currentAction)
        {
            case CharacterAction.IDLE:
                PlayAnimation(idleSprites, frameRate);
                break;
            case CharacterAction.ATTACK:
                PlayAnimation(attackSprites, frameRate);
                break;
            case CharacterAction.DEFENSE:
                PlayAnimation(defendSprites, frameRate);
                break;
            default:
                PlayAnimation(idleSprites, frameRate); // 默認閒置
                break;
        }
    }
}
