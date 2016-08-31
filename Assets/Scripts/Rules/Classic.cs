using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classic : Ruleset
{
    internal new enum States
    {
        [FloatValue(0.35f)]
        [ColorValue(0.8f, 0.8f, 0.8f, 1.0f)]
        Alive,

        [FloatValue(0.65f)]
        [ColorValue(0.2f, 0.2f, 0.2f, 1.0f)]
        Dead
    }

    public Classic() : base()
    {
        rules.Add(new Birth());
        rules.Add(new Death());
    }
}

class Birth : IRule
{
    public int Process(int[] count, int current_state)
    {
        int next_state = current_state;

        if (current_state == (int) Classic.States.Dead)
        {
            if (count[(int) Classic.States.Alive] == 3)
                next_state = (int)Classic.States.Alive;
        }

        return next_state;
    }
}

class Death : IRule
{
    public int Process(int[] count, int current_state)
    {
        int next_state = current_state;

        if (current_state == (int)Classic.States.Alive)
        {
            if (count[(int)Classic.States.Alive] < 2 || count[(int)Classic.States.Alive] > 3)
                next_state = (int)Classic.States.Dead;
        }

        return next_state;
    }
}

