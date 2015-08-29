using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HaveInputs : AbstractManager, IInputManager 
{

    public int _playerIndex = 1;
    public string _horizontalAxis = "Horizontal";
    public string _verticalAxis = "Vertical";
    public string _skillPrefix = "Fire";

    public ControlMode _mode = ControlMode.keyboard;
    public enum ControlMode { keyboard, mouse, game_controller }
    Dictionary<ControlMode, string> ControlModeToString = new Dictionary<ControlMode, string> 
    { 
        { ControlMode.keyboard, "K" },
        { ControlMode.mouse, "Mouse" },
        { ControlMode.game_controller, "C" }
    };

    public Vector2 Direction
    {
        get
        {
            float horizontal;
            float vertical;

            switch (_mode)
            {
                case ControlMode.mouse :
                    if (Vector3.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position) <= 2.5f) 
                        return Vector2.zero;
                    horizontal =  Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
                    vertical = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
                    return new Vector2(horizontal, vertical).normalized;
                default :
                    horizontal = Input.GetAxis(ControlPrefix + _horizontalAxis);
                    vertical = Input.GetAxis(ControlPrefix + _verticalAxis);
                    return new Vector2(horizontal, vertical);
            }
        }
    }

    public bool this[int index]
    {
        get
        {
            switch (_mode)
            {
                case ControlMode.mouse:
                    if (index == 1 && Input.GetMouseButtonDown(0)) return true;
                    else if (index == 2 && Input.GetMouseButtonDown(1)) return true;
                    else if (index == 3 && Input.GetMouseButtonDown(2)) return true;
                    else return false;
                default:
                    return Input.GetButtonDown(ControlPrefix + _skillPrefix + index);
            }
        }
    }

    private string ControlPrefix
    {
        get
        {
            return ControlModeToString[_mode] + _playerIndex.ToString() + "_";
        }
    }
}
