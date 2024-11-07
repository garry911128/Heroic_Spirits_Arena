using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory
{
    public Character CreateCharacter(CharacterType characterType)
    {
        Character character;
        string characterPath = "Character/" + characterType.ToString();

        Sprite[] idleSprites = LoadAnimationSprites(characterPath + "/Idle");
        Sprite[] attackSprites = LoadAnimationSprites(characterPath + "/Attack");
        Sprite[] defendSprites = LoadAnimationSprites(characterPath + "/Defense");

        // 根據角色類型創建實例
        switch (characterType)
        {
            case CharacterType.Saber:
                character = new Saber();
                break;
            case CharacterType.Archer:
                character = new Archer();
                break;
            case CharacterType.Paladin:
                character = new Paladin();
                break;
            default:
                Debug.LogError("Unknown CharacterType: " + characterType);
                return null;
        }

        // 設置角色的動畫圖片數組
        character.idleSprites = idleSprites;
        character.attackSprites = attackSprites;
        character.defendSprites = defendSprites;

        return character;
    }

    // 通用方法來加載動畫圖片
    private Sprite[] LoadAnimationSprites(string path)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(path);
        if (sprites == null || sprites.Length == 0)
        {
            Debug.LogError("Failed to load sprites at path: " + path);
            return null;
        }
        return sprites;
    }
}
