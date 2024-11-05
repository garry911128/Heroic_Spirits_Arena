using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class CharacterFactory
{
    public Character CreateCharacter(CharacterType characterType)
    {
        Character character;
        string imagePath = "Character/" + characterType.ToString() + "/" + characterType.ToString();
        Sprite charactSprite = Resources.Load<Sprite>(imagePath);
        string animationPath = "Character/" + characterType.ToString() + "/" + characterType.ToString() + "Attack";
        Animator attackAnimation = Resources.Load<Animator>(animationPath);
        if (charactSprite == null)
        {
            Debug.LogError("Failed to load sprite at path: " + imagePath);
            return null;
        }

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
        
        character.sprite = charactSprite;
        character.animator = attackAnimation;
        return character;
    }
}
