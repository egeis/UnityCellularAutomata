using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityToolbag;

public class Classic
{
    public enum States
    {
        [FloatValue(0.65f)]
        [ColorValue(0.2f, 0.2f, 0.2f, 1.0f)]
        Dead = 0,

        [FloatValue(0.35f)]
        [ColorValue(0.8f, 0.8f, 0.8f, 1.0f)]
        Alive = 1
    }

    //Weighted Values
    List<WeightedStates> weights = new List<WeightedStates>();
    float[] cumValues;
    float cumValueLast;

    public Classic() 
    {
        WeightedStates lws = null;

        int index = 0;
        cumValues = new float[Enum.GetNames(typeof(States)).Length];

        foreach (States val in Enum.GetValues(typeof(States)))
        {
            WeightedStates ws = new WeightedStates();
            ws.Id = (int) val;
            ws.Weight = getFloatValue((int) val);

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

    public Future<Dictionary<Vector2, int>> ComputeNextState(Dictionary<Vector2, int> lastState, Vector2 bounds)
    {
        Future<Dictionary<Vector2, int>> future = new Future<Dictionary<Vector2, int>>();

        future.Process(() =>
        {
            Dictionary<Vector2, int> states = new Dictionary<Vector2, int>();

            foreach (Vector2 key in lastState.Keys)
            {
                int value = 0;
                lastState.TryGetValue(key, out value);
                int[] count = new int[Enum.GetNames(typeof(States)).Length];

                float[] xs = new float[]
                {
                    MathHelper.mod(key.x + 1, bounds.x),
                    MathHelper.mod(key.x + 1, bounds.x),
                    key.x,
                    key.x,
                    MathHelper.mod(key.x + 1, bounds.x),
                    MathHelper.mod(key.x - 1, bounds.x),
                    MathHelper.mod(key.x - 1, bounds.x),
                    MathHelper.mod(key.x - 1, bounds.x)
                };

                float[] ys = new float[]
                {
                    key.y,
                    MathHelper.mod(key.y + 1, bounds.y),
                    MathHelper.mod(key.y + 1, bounds.y),
                    MathHelper.mod(key.y - 1, bounds.y),
                    MathHelper.mod(key.y - 1, bounds.y),
                    key.y,
                    MathHelper.mod(key.y + 1, bounds.y),
                    MathHelper.mod(key.y - 1, bounds.y)
                };

                for (int i = 0; i < 8; i++)
                {
                    Vector2 v = new Vector2(xs[i], ys[i]);
                    int a = (int) States.Dead;

                    bool success = lastState.TryGetValue(v, out a);

                    if (success)
                        count[a] += 1;
                }

                if (value == (int)States.Alive)
                    value = Death.Process(count, value);
                else
                    value = Birth.Process(count, value);

                states.Add(key, value);
            }
            return states;
        });

        return future;
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

static class Birth
{
    public static int Process(int[] count, int current_state)
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

static class Death
{
    public static int Process(int[] count, int current_state)
    {
        int next_state = current_state;

        if (current_state == (int) Classic.States.Alive)
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

