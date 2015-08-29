using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HaveAnimations : AnimationHandler
{
    private string _folderName;
    public string FolderName { get { return _folderName; } set { _folderName = value; _path = _folderName + "/" + _spriteName; } }

    private string _spriteName;
    public string SpriteName { get { return _spriteName; } set { _spriteName = value; _path = _folderName + "/" + _spriteName; } }

    private string _path = "SpriteSheets/Goblin";

    #region Enum

    public enum Animations
    {
        idle,
        walk,
        hurt,
        dead,
        attack
    }

    public enum Directions
    {
        up,
        down,
        left,
        right
    }

    #endregion

    #region EnumToString_Dictionnaries

    protected Dictionary<Animations, string> AnimToString = new Dictionary<Animations, string>
    {
        { Animations.idle, "Idle"},
        { Animations.walk, "Walk"},
        { Animations.hurt, "Hurt"},
        { Animations.dead, "Dead"},
        { Animations.attack, "Attack"},
    };

    protected Dictionary<Directions, string> DirToString = new Dictionary<Directions, string>
    {
        { Directions.up, "Up"},
        { Directions.down, "Down"},
        { Directions.left, "Left"},
        { Directions.right, "Right"},
    };

    #endregion


    public void PlayInVoid(string animationName)
    {
        GameObject go = new GameObject();
        go.AddComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        HaveAnimations anim = go.AddComponent<HaveAnimations>();
        anim.Play(animationName, true);
        Destroy(go, 2);//anim.currentAnimationLength);
    }

    public void Play(string animationName, bool looping = true)
    {
        PlayClip(_path + animationName, looping);
    }

    public void Play(string animationName, Vector2 direction, bool looping = true)
    {
        Play(animationName + DirToString[Directions.down], looping);
    }

    public void Play(Animations animation, bool looping = true)
    {
        Play(AnimToString[animation] + DirToString[Directions.down], looping);
    }

    public void Play(Animations animation, Directions direction, bool looping = true)
    {
        Play(AnimToString[animation] + DirToString[direction], looping);
    }

    public void Play(Animations animation, Vector2 directionVector, bool looping = true)
    {
        Directions direction = Directions.down;

        float absX = Mathf.Abs(directionVector.x);
        float absY = Mathf.Abs(directionVector.y);

        if (absX > absY)
        {
            if (directionVector.x > 0)
                direction = Directions.right;
            else if (directionVector.x < 0)
                direction = Directions.left;
        }
        else if (absX < absY)
        {
            if (directionVector.y > 0)
                direction = Directions.up;
            else if (directionVector.y < 0)
                direction = Directions.down;
        }
        else if (absX == absY)
        {
            if (directionVector.y > 0)
                direction = Directions.up;
            else if (directionVector.y < 0)
                direction = Directions.down;
        }

        Play(animation, direction, looping);
    }

}
