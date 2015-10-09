using UnityEngine;
using System.Collections;

public interface IManagerInput
{
    Vector2 Direction { get; }

    bool this[int index] { get; }
}
