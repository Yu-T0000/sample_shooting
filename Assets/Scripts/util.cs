using UnityEngine;

public static class util
{

    public static Vector3 ClampPosition(Vector3 position,Vector2 moveLimit){
        return new Vector3(Mathf.Clamp(position.x, -moveLimit.x, moveLimit.x),
        Mathf.Clamp(position.y, -moveLimit.y, moveLimit.y), 0);
    }

    public static Vector3 GetDirection(float angle){
        return new Vector3(
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad),
            0
        );
    }

    public static float GetAngle( Vector2 from, Vector2 to )
{
    var dx = to.x - from.x;
    var dy = to.y - from.y;
    var rad = Mathf.Atan2( dy, dx );
    return rad * Mathf.Rad2Deg;
}

}
