using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    public Sprite[] attackSprites; // Array to hold attack animation sprites
    public Sprite[] defenseSprites; // Array to hold defense animation sprites
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on this GameObject.");
        }
    }

    // Method to load the sprites from resources
    public void LoadAnimationSprites(CharacterType characterType)
    {
        // Load attack sprites
        attackSprites = Resources.LoadAll<Sprite>($"Characters/{characterType}/Attack");
        // Load defense sprites
        defenseSprites = Resources.LoadAll<Sprite>($"Characters/Defense");
    }

    // Method to play the attack animation
    public void PlayAttackAnimation(float animationSpeed = 0.1f)
    {
        StartCoroutine(PlayAnimation(attackSprites, animationSpeed));
    }

    // Method to play the defense animation
    public void PlayDefenseAnimation(float animationSpeed = 0.1f)
    {
        StartCoroutine(PlayAnimation(defenseSprites, animationSpeed));
    }

    // Coroutine to play the animation
    private IEnumerator PlayAnimation(Sprite[] animationSprites, float animationSpeed)
    {
        if (animationSprites == null || animationSprites.Length == 0)
        {
            Debug.LogWarning("No sprites found for animation.");
            yield break; // Exit if there are no sprites to animate
        }

        for (int i = 0; i < animationSprites.Length; i++)
        {
            spriteRenderer.sprite = animationSprites[i]; // Set the current sprite
            yield return new WaitForSeconds(animationSpeed); // Wait for the specified duration
        }

        // Optionally, reset to the idle state or a default sprite after the animation
        // spriteRenderer.sprite = idleSprite; // You may need to set an idle sprite here
    }
}

