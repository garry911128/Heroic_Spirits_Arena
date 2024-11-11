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
        float frameDuration = 0.1f;  // �C�V����ɶ��A�i�ۦ�վ�

        switch (action)
        {
            case CharacterAction.ATTACK:
                currentActionSprites = attackSprites; // �����ʵe
                break;
            case CharacterAction.DEFENSE:
                currentActionSprites = defendSprites; // ���m�ʵe
                break;
            case CharacterAction.IDLE:
            default:
                currentActionSprites = idleSprites; // �ݾ��ʵe
                break;
        }

        for (int i = 0; i < currentActionSprites.Length; i++)
        {
            characterImage.sprite = currentActionSprites[i];
            yield return new WaitForSeconds(frameDuration);
        }

        currentActionSprites = idleSprites; // ���񧹲�������^�ݾ����A
        characterImage.sprite = currentActionSprites[0];
    }
}
