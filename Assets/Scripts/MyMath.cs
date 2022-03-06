/// <summary>
/// A static class with custom math functions
/// </summary>
public static class MyMath {
    /// <summary>
    /// Round to a multiple of a value
    /// </summary>
    /// <param name="value"></param>
    /// <param name="multipleOf"></param>
    /// <returns></returns>
    public static int RoundToMultiple(int value, int multipleOf) {
        return ((int)(value / multipleOf + 0.5f) * multipleOf);
    }
    
    /// <summary>
    /// Round to a multiple of a value
    /// </summary>
    /// <param name="value"></param>
    /// <param name="multipleOf"></param>
    /// <returns></returns>
    public static float RoundToMultiple(float value, float multipleOf) {
        return (int)(value / multipleOf + 0.5f) * multipleOf;
    }
}
