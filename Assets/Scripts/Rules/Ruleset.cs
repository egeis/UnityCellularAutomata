using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruleset
{
    internal enum States {
        [FloatValue(1.0f)]              //Weight (Total of all enums == 1f)
        [ColorValue(1f, 1f, 1f, 1f)]    //Color
        Alive,                          //ID
    }

    //Weighted Values
    List<WeightedStates> weights = new List<WeightedStates>();
    float[] cumValues;
    float cumValueLast;

    //Futures
    protected List<IRule> rules = new List<IRule>();

    public Ruleset() {
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

class WeightedStates
{
    public int Id { get; set; }
    public float Weight { get; set; }
    public float Cumulative { get; set; }
}

public interface IRule { int Process(int[] count, int current_state); }