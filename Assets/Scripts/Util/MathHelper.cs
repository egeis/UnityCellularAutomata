using System;

public static class MathHelper
{
    public static float mod(float a, float b)
    {
        return (a % b + b) % b;
    }

    public static float Lerp(float from, float to, float value)
    {
        if (value < 0.0f)
            return from;
        else if (value > 1.0f)
            return to;
        return (to - from) * value + from;
    }

    public static float LerpUnclamped(float from, float to, float value)
    {
        return (1.0f - value) * from + value * to;
    }
}