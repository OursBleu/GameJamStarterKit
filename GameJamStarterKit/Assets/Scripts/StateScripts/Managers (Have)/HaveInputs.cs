using UnityEngine;
using System.Collections;

public class HaveInputs : AbstractManager, IInputManager 
{

    public int _playerIndex = 0;
    public string _horizontalAxis = "Horizontal";
    public string _verticalAxis = "Vertical";
    public string _skillPrefix = "Skill ";

    public Vector2 Direction
    {
        get
        {
            float horizontal = Input.GetAxis(_horizontalAxis + (_playerIndex != 0 ? _playerIndex.ToString() : ""));
            float vertical = Input.GetAxis(_verticalAxis + (_playerIndex != 0 ? _playerIndex.ToString() : ""));
            return new Vector2(horizontal, vertical);
        }
    }

    public bool this[int index]
    {
        get
        {
            return Input.GetButtonDown(_skillPrefix + index + (_playerIndex != 0 ? _playerIndex.ToString() : ""));
        }
    }
}
