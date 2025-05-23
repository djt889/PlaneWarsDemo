using System;
using UnityEngine;

public class KeyInterval
{
    private readonly float _invoke; //间隔时间
    private float _invokeTimer; //间隔计时器
    private readonly KeyCode _keyCode; //按键

    //构造函数，初始化按键和间隔时间
    public KeyInterval(KeyCode keyCode, float invoke)
    {
        _keyCode = keyCode;
        _invoke = invoke;
        _invokeTimer = invoke;
    }

    //按键触发函数，传入两个Action，一个用于按下时触发，一个用于抬起时触发
    public void KeyInvoke(Action action, Action actionUp)
    {
        //如果按键被按下
        if (Input.GetKey(_keyCode))
        {
            //增加间隔计时器
            _invokeTimer += Time.deltaTime;
            //如果间隔计时器大于等于间隔时间
            if (_invokeTimer >= _invoke)
            {
                //执行按下时的Action
                action();
                //重置间隔计时器
                _invokeTimer = 0;
            }
        }

        //如果按键被抬起
        if (Input.GetKeyUp(_keyCode))
        {
            //重置间隔计时器
            _invokeTimer = _invoke;
            //如果抬起时的Action不为空
            if (actionUp != null) actionUp();
        }
    }
}