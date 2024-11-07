using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAnimator : MonoBehaviour
{
    public Image characterImage; // �Ψ���ܨ���Ϥ��� Image ����
    public Sprite[] idleSprites; // ���m���A���Ϥ��ǦC
    public Sprite[] attackSprites; // �������A���Ϥ��ǦC
    public Sprite[] defendSprites; // ���m���A���Ϥ��ǦC

    private Sprite[] currentActionSprites; // ��e���A���Ϥ��ǦC
    private int currentFrame = 0; // ��e�V
    private bool isAnimating = false; // �O�_���b����ʵe

    // ����ʵe
    public void PlayAnimation(Sprite[] sprites, float frameRate)
    {
        if (isAnimating)
            return; // �p�G�w�g�b����ʵe�A�h���A����

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
            yield return new WaitForSeconds(frameRate); // �ھڴV�v����
        }

        isAnimating = false; // �ʵe���񵲧�
        currentActionSprites = idleSprites;
        characterImage.sprite = currentActionSprites[0];
    }

    // ��s�ʵe�ھڨ��⪺��e���A
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
                PlayAnimation(idleSprites, frameRate); // �q�{���m
                break;
        }
    }
}
