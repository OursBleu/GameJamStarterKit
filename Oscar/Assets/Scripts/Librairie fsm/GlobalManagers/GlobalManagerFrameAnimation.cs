using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationStruct
{
    public SpriteRenderer _target;
    public string _path;
    public float _length;
    public Coroutine _coroutine;
}

public class GlobalManagerFrameAnimation : MonoBehaviour
{
    Dictionary<SpriteRenderer, AnimationStruct> _animations;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _animations = new Dictionary<SpriteRenderer, AnimationStruct>();
    }

    public bool PlayClip(SpriteRenderer targetRenderer, string path, bool looping = false, int fps = 10)
    {
        // DON'T PLAY THE SAME ANIMATION ON TOP OF ITSELF, STOP THE PREVIOUS ANIMATION

        if (!_animations.ContainsKey(targetRenderer)) _animations.Add(targetRenderer, null);
        else if (_animations[targetRenderer]._path == path) return true;
        else StopCoroutine(_animations[targetRenderer]._coroutine);

        AnimationStruct animation = new AnimationStruct();
        animation._target = targetRenderer;
        animation._path = path;
        _animations[targetRenderer] = animation;

        // LOAD THE SPRITESHEET AND AVERT USER IF THE IMPORT FAILED

        Sprite[] frames = Resources.LoadAll<Sprite>(path);

        if (frames.Length == 0)
        {
            //Debug.Log("Resources/" + spriteSheetPath + " not found.");
            return false;
        }

        // LAUNCH LOOPING ANIMATION OR SINGLE ANIMATION COROUTINE

        if (looping)
        {
            animation._length = Mathf.Infinity;
            animation._coroutine = StartCoroutine(PlayForever(targetRenderer, frames, fps));
        }
        else
        {
            animation._length = frames.Length * (1f / fps);
            animation._coroutine = StartCoroutine(PlayOnce(targetRenderer, frames, fps));
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
        _animations.Remove(renderer);
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
