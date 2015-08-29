using UnityEngine;
using System.Collections;

public interface IInputManager
{
    Vector2 Direction { get; }

    bool this[int index] { get; }
}
