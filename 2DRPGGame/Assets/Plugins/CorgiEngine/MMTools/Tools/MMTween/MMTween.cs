using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMTween : MonoBehaviour
{
    public enum MMTweenCurve
    {
        LinearTween,
        EaseInQuadratic,
        EaseOutQuadratic,
        EaseInOutQuadratic,
        EaseInCubic,
        EaseOutCubic,
        EaseInOutCubic,
        EaseInQuartic,
        EaseOutQuartic,
        EaseInOutQuartic,
        EaseInQuintic,
        EaseOutQuintic,
        EaseInOutQuintic,
        EaseInSinusoidal,
        EaseOutSinusoidal,
        EaseInOutSinusoidal,
        EaseInBounce,
        EaseOutBounce,
        EaseInOutBounce,
        EaseInOverhead,
        EaseOutOverhead,
        EaseInOutOverhead,
        EaseInExponential,
        EaseOutExponential,
        EaseInOutExponential,
        EaseInElastic,
        EaseOutElastic,
        EaseInOutElastic,
        EaseInCircular,
        EaseOutCircular,
        EaseInOutCircular,
        AntiLinearTween,
        AlmostIdentity
    }

    public static float Tween(float currentTime, float initialTime, float endTime, float startValue, float endValue,
        MMTweenCurve curve)
    {
        //将区间[initialTime，endTime]中的值currentTime重映射为区间[0f，1f]中的比例值
        currentTime = MMMaths.Remap(currentTime, initialTime, endTime, 0f, 1f);
        switch (curve)
        {
            //线性运动，所以CurrentTime不用做任何变化
            case MMTweenCurve.LinearTween:
                currentTime = MMTweenDefinitions.Linear_Tween(currentTime);
                break;
            case MMTweenCurve.AntiLinearTween:
                currentTime = MMTweenDefinitions.LinearAnti_Tween(currentTime);
                break;

            case MMTweenCurve.EaseInQuadratic:
                currentTime = MMTweenDefinitions.EaseIn_Quadratic(currentTime);
                break;
            case MMTweenCurve.EaseOutQuadratic:
                currentTime = MMTweenDefinitions.EaseOut_Quadratic(currentTime);
                break;
            case MMTweenCurve.EaseInOutQuadratic:
                currentTime = MMTweenDefinitions.EaseInOut_Quadratic(currentTime);
                break;

            case MMTweenCurve.EaseInCubic:
                currentTime = MMTweenDefinitions.EaseIn_Cubic(currentTime);
                break;
            case MMTweenCurve.EaseOutCubic:
                currentTime = MMTweenDefinitions.EaseOut_Cubic(currentTime);
                break;
            case MMTweenCurve.EaseInOutCubic:
                currentTime = MMTweenDefinitions.EaseInOut_Cubic(currentTime);
                break;

            case MMTweenCurve.EaseInQuartic:
                currentTime = MMTweenDefinitions.EaseIn_Quartic(currentTime);
                break;
            case MMTweenCurve.EaseOutQuartic:
                currentTime = MMTweenDefinitions.EaseOut_Quartic(currentTime);
                break;
            case MMTweenCurve.EaseInOutQuartic:
                currentTime = MMTweenDefinitions.EaseInOut_Quartic(currentTime);
                break;
            case MMTweenCurve.EaseInQuintic:
                currentTime = MMTweenDefinitions.EaseIn_Quintic(currentTime);
                break;
            case MMTweenCurve.EaseOutQuintic:
                currentTime = MMTweenDefinitions.EaseOut_Quintic(currentTime);
                break;
            case MMTweenCurve.EaseInOutQuintic:
                currentTime = MMTweenDefinitions.EaseInOut_Quintic(currentTime);
                break;
            case MMTweenCurve.EaseInSinusoidal:
                currentTime = MMTweenDefinitions.EaseIn_Sinusoidal(currentTime);
                break;
            case MMTweenCurve.EaseOutSinusoidal:
                currentTime = MMTweenDefinitions.EaseOut_Sinusoidal(currentTime);
                break;
            case MMTweenCurve.EaseInOutSinusoidal:
                currentTime = MMTweenDefinitions.EaseInOut_Sinusoidal(currentTime);
                break;
            case MMTweenCurve.EaseInBounce:
                currentTime = MMTweenDefinitions.EaseIn_Bounce(currentTime);
                break;
            case MMTweenCurve.EaseOutBounce:
                currentTime = MMTweenDefinitions.EaseOut_Bounce(currentTime);
                break;
            case MMTweenCurve.EaseInOutBounce:
                currentTime = MMTweenDefinitions.EaseInOut_Bounce(currentTime);
                break;

            case MMTweenCurve.EaseInOverhead:
                currentTime = MMTweenDefinitions.EaseIn_Overhead(currentTime);
                break;
            case MMTweenCurve.EaseOutOverhead:
                currentTime = MMTweenDefinitions.EaseOut_Overhead(currentTime);
                break;
            case MMTweenCurve.EaseInOutOverhead:
                currentTime = MMTweenDefinitions.EaseInOut_Overhead(currentTime);
                break;
            case MMTweenCurve.EaseInExponential:
                currentTime = MMTweenDefinitions.EaseIn_Exponential(currentTime);
                break;
            case MMTweenCurve.EaseOutExponential:
                currentTime = MMTweenDefinitions.EaseOut_Exponential(currentTime);
                break;
            case MMTweenCurve.EaseInOutExponential:
                currentTime = MMTweenDefinitions.EaseInOut_Exponential(currentTime);
                break;
            case MMTweenCurve.EaseInElastic:
                currentTime = MMTweenDefinitions.EaseIn_Elastic(currentTime);
                break;
            case MMTweenCurve.EaseOutElastic:
                currentTime = MMTweenDefinitions.EaseOut_Elastic(currentTime);
                break;
            case MMTweenCurve.EaseInOutElastic:
                currentTime = MMTweenDefinitions.EaseInOut_Elastic(currentTime);
                break;
            case MMTweenCurve.EaseInCircular:
                currentTime = MMTweenDefinitions.EaseIn_Circular(currentTime);
                break;
            case MMTweenCurve.EaseOutCircular:
                currentTime = MMTweenDefinitions.EaseOut_Circular(currentTime);
                break;
            case MMTweenCurve.EaseInOutCircular:
                currentTime = MMTweenDefinitions.EaseInOut_Circular(currentTime);
                break;
            case MMTweenCurve.AlmostIdentity:
                currentTime = MMTweenDefinitions.AlmostIdentity(currentTime);
                break;

        }

        return startValue + currentTime * (endValue - startValue);
    }

    public static float Tween(float currentTime, float initialTime, float endTime, float startValue, float endValue, AnimationCurve curve)
    {
        currentTime = MMMaths.Remap(currentTime, initialTime, endTime, 0f, 1f);
        currentTime = curve.Evaluate(currentTime);
        return startValue + currentTime * (endValue - startValue);
    }

    public static float Tween(float currentTime, float initialTime, float endTime, float startValue, float endValue,
        MMTweenType tweenType)
    {
        if (tweenType.MMTweenDefinitionType == MMTweenDefinitionTypes.MMTween)
        {
            return Tween(currentTime, initialTime, endTime, startValue, endValue, tweenType.MMTweenCurve);
        }

        if (tweenType.MMTweenDefinitionType == MMTweenDefinitionTypes.AnimationCurve)
        {
            return Tween(currentTime, initialTime, endTime, startValue, endValue, tweenType.Curve);
        }

        Debug.Log("MMTween------Tween----3");
        return 0f;
    }
}
