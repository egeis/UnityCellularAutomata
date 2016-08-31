using System;

public static class MathHelper
{
    public static float mod(float a, float b)
    {
        return (a % b + b) % b;
    }
}