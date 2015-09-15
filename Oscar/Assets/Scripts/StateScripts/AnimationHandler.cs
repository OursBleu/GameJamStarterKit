using UnityEngine;
using System.Collections;

public class AnimationHandler : Manager
{
    Coroutine currentAnimation;
    string currentAnimationPath;
    public float currentAnimationLength = 0f;

    void Test()
    {
        PlayClip("SpriteSheets/GoblinWalkLeft");
    }

    protected bool PlayClip(string spriteSheetPath,  bool looping = false, int fps = 10)
    {

        // DON'T PLAY THE SAME ANIMATION ON TOP OF ITSELF, STOP THE PREVIOUS ANIMATION

        if (currentAnimationPath == spriteSheetPath) return true;
        currentAnimationPath = spriteSheetPath;

        if (currentAnimation != null) StopCoroutine(currentAnimation);

        // LOAD THE SPRITESHEET AND AVERT USER IF THE IMPORT FAILED

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Sprite[] frames = Resources.LoadAll<Sprite>(spriteSheetPath);

        if (frames.Length == 0)
        {
            //Debug.Log("Resources/" + spriteSheetPath + " not found.");
            return false;
        }

        // LAUNCH LOOPING ANIMATION OR SINGLE ANIMATION COROUTINE

        if (looping)
        {
            currentAnimationLength = Mathf.Infinity;
            currentAnimation = StartCoroutine(PlayForever(renderer, frames, fps));
        }
        else
        {
            currentAnimationLength = frames.Length * (1f / fps);
            currentAnimation = StartCoroutine(PlayOnce(renderer, frames, fps));
        }

        return true;

    }

    IEnumerator PlayOnce(SpriteRenderer renderer, Sprite[] frames, int fps)
    { 
        float delayBetweenFrames = 1f / fps;
        foreach (Sprite frame in frames)
        {
            renderer.sprite = frame;
            yield return new WaitForSeconds(delayBetweenFrames);
        }
    }

    IEnumerator PlayForever(SpriteRenderer renderer, Sprite[] frames, int fps)
    {
        float delayBetweenFrames = 1f / fps;
        int i = 0;
        while (true)
        {
            renderer.sprite = frames[i++ % frames.Length];
            yield return new WaitForSeconds(delayBetweenFrames);
        }
    }
}
