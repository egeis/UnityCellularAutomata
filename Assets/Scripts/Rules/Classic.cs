using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classic
{
    public enum States
    {
        [FloatValue(0.35f)]
        [ColorValue(0.8f, 0.8f, 0.8f, 1.0f)]
        Alive,

        [FloatValue(0.65f)]
        [ColorValue(0.2f, 0.2f, 0.2f, 1.0f)]
        Dead
    }

    //Weighted Values
    List<WeightedStates> weights = new List<WeightedStates>();
    float[] cumValues;
    float cumValueLast;

    //Futures
    protected List<IRule> rules = new List<IRule>();

    public Classic() 
    {
        rules.Add(new Birth());
        rules.Add(new Death());

        WeightedStates lws = null;

        int index = 0;
        cumValues = new float[Enum.GetNames(typeof(States)).Length];

        foreach (States val in Enum.GetValues(typeof(States)))
        {
            WeightedStates ws = new WeightedStates();
            ws.Id = (int)val;
            ws.Weight = getFloatValue((int)val);

            if (lws == null)
                ws.Cumulative = ws.Weight;
            else
                ws.Cumulative = ws.Weight + lws.Cumulative;

            weights.Add(ws);
            lws = ws;
            cumValues[index++] = ws.Cumulative;
        }

        cumValueLast = cumValues[cumValues.Length - 1];
    }

    public Color getColorValue(int value)
    {
        return ColorEnum.getColorValue((States)value);
    }

    public float getFloatValue(int value)
    {
        return FloatEnum.getFloatValue((States)value);
    }

    public int getRandomCell()
    {
        float value = UnityEngine.Random.value * cumValueLast;

        int index = Array.BinarySearch(cumValues, value);

        if (index <= 0)
            index = ~index;

        return weights[index].Id;
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

class WeightedStates
{
    public int Id { get; set; }
    public float Weight { get; set; }
    public float Cumulative { get; set; }
}

public interface IRule { int Process(int[] count, int current_state); }

