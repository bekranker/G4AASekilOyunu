using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public static class StaticTweenFunctions
{
    public static Tween MyScaleHandler(this Transform transform, Vector3 targetScale, float duration)
    {
        return transform.DOScale(targetScale, duration).SetUpdate(true);
    }
    public static Tween MyScalePunchHandler(this Transform transform, Vector3 punch, float duration)
    {
        return transform.DOPunchScale(punch, duration).SetUpdate(true);
    }
    public static Tween MyRotationHandler(this Transform transform, Vector3 targetRotation, float duration)
    {
        return transform.DORotate(targetRotation, duration).SetUpdate(true);
    }
    public static Tween MyRandomRotationHandler(this Transform transform, Vector3 targetRotation, float duration)
    {
        return transform.DORotate(targetRotation, duration, RotateMode.LocalAxisAdd).SetUpdate(true);
    }
    public static Tween MyRotationPunchHandler(this Transform transform, Vector3 targetRotation, float duration)
    {
        return transform.DOPunchRotation(targetRotation, duration).SetUpdate(true);
    }
}
