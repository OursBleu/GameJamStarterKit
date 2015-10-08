using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerInputPlayer : Manager, IManagerInput, IDisableable
{

    [SerializeField] int _playerIndex = 1;

    [SerializeField] ControlMode _mode = ControlMode.keyboard;

    enum ControlMode { keyboard, mouse, game_controller }

    Dictionary<ControlMode, string> ControlModeToString = new Dictionary<ControlMode, string> 
    { 
        { ControlMode.keyboard, "K" },
        { ControlMode.mouse, "Mouse" },
        { ControlMode.game_controller, "C" }
    };

    string _horizontalAxis = "Horizontal";
    string _verticalAxis = "Vertical";
    string _skillPrefix = "Fire";

    private int _disablingStacks = 0;
    private bool _isEnabled = true;
    public bool IsEnabled
    {
        get { return _isEnabled; }
        set
        {
            if (value == false) _disablingStacks++;
            else _disablingStacks--;
            _isEnabled = (_disablingStacks <= 0);
        }
    }

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
            if (!_isEnabled) return false;

            switch (_mode)
            {
                case ControlMode.mouse:
                    if (index == 0 && Input.GetMouseButtonDown(0)) return true;
                    else if (index == 1 && Input.GetMouseButtonDown(1)) return true;
                    else if (index == 2 && Input.GetMouseButtonDown(2)) return true;
                    else return false;
                default:
                    return Input.GetButtonDown(ControlPrefix + _skillPrefix + (index+1));
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

    public override string ToString()
    {
        string res = "Input : ";
        res += "(" + Direction.x.ToString("F1") + "," + Direction.y.ToString("F1") + ") ";
        for (int i = 1; i < 4; i++ ) if (this[i]) res += ControlPrefix + _skillPrefix + i +" ";
        return res + "\n";
    }
}
