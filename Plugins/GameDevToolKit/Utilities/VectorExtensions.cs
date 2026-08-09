using UnityEngine;

public static class VectorExtensions
{
    // Vector 2 - Swizzling (x, y)
    public static Vector2 xy(this Vector2 v) => new Vector2(v.x, v.y);

    public static Vector2 yx(this Vector2 v) => new Vector2(v.y, v.x);

    // Vector 2 - Flattening & Swizzling

    public static Vector2 flatten_x0(this Vector2 v) => new Vector2(v.x, 0.0f);

    public static Vector2 flatten_y0(this Vector2 v) => new Vector2(v.y, 0.0f);

    public static Vector2 flatten_0x(this Vector2 v) => new Vector2(0.0f, v.x);

    public static Vector2 flatten_0y(this Vector2 v) => new Vector2(0.0f, v.y);

    // Vector 3 Swizzling (x, y, z)
    public static Vector2 xy(this Vector3 v) => new Vector2(v.x, v.y);

    public static Vector2 xz(this Vector3 v) => new Vector2(v.x, v.z);

    public static Vector2 yz(this Vector3 v) => new Vector2(v.y, v.z);

    public static Vector2 yx(this Vector3 v) => new Vector2(v.y, v.x);

    public static Vector2 zx(this Vector3 v) => new Vector2(v.z, v.x);

    public static Vector2 zy(this Vector3 v) => new Vector2(v.z, v.y);

    public static Vector3 xyz(this Vector3 v) => new Vector3(v.x, v.y, v.z);

    public static Vector3 xzy(this Vector3 v) => new Vector3(v.x, v.z, v.y);

    public static Vector3 yxz(this Vector3 v) => new Vector3(v.y, v.x, v.z);

    public static Vector3 yzx(this Vector3 v) => new Vector3(v.y, v.z, v.x);

    public static Vector3 zxy(this Vector3 v) => new Vector3(v.z, v.x, v.y);

    public static Vector3 zyx(this Vector3 v) => new Vector3(v.z, v.y, v.x);

    // Vector 3 - Flattening One & Swizzling

    public static Vector3 flatten_xy0(this Vector3 v) => new Vector3(v.x, v.y, 0.0f);

    public static Vector3 flatten_xz0(this Vector3 v) => new Vector3(v.x, v.z, 0.0f);

    public static Vector3 flatten_yz0(this Vector3 v) => new Vector3(v.y, v.z, 0.0f);

    public static Vector3 flatten_yx0(this Vector3 v) => new Vector3(v.y, v.x, 0.0f);

    public static Vector3 flatten_zx0(this Vector3 v) => new Vector3(v.z, v.x, 0.0f);

    public static Vector3 flatten_zy0(this Vector3 v) => new Vector3(v.z, v.y, 0.0f);

    public static Vector3 flatten_x0y(this Vector3 v) => new Vector3(v.x, 0.0f, v.y);

    public static Vector3 flatten_x0z(this Vector3 v) => new Vector3(v.x, 0.0f, v.z);

    public static Vector3 flatten_y0z(this Vector3 v) => new Vector3(v.y, 0.0f, v.z);

    public static Vector3 flatten_y0x(this Vector3 v) => new Vector3(v.y, 0.0f, v.x);

    public static Vector3 flatten_z0x(this Vector3 v) => new Vector3(v.z, 0.0f, v.x);

    public static Vector3 flatten_z0y(this Vector3 v) => new Vector3(v.z, 0.0f, v.y);

    public static Vector3 flatten_0xy(this Vector3 v) => new Vector3(0.0f, v.x, v.y);

    public static Vector3 flatten_0xz(this Vector3 v) => new Vector3(0.0f, v.x, v.z);

    public static Vector3 flatten_0yz(this Vector3 v) => new Vector3(0.0f, v.y, v.z);

    public static Vector3 flatten_0yx(this Vector3 v) => new Vector3(0.0f, v.y, v.x);

    public static Vector3 flatten_0zx(this Vector3 v) => new Vector3(0.0f, v.z, v.x);

    public static Vector3 flatten_0zy(this Vector3 v) => new Vector3(0.0f, v.z, v.y);

    // Vector 3 - Flattening Two & Swizzling

    public static Vector3 flatten_x00(this Vector3 v) => new Vector3(v.x, 0.0f, 0.0f);

    public static Vector3 flatten_y00(this Vector3 v) => new Vector3(v.y, 0.0f, 0.0f);

    public static Vector3 flatten_z00(this Vector3 v) => new Vector3(v.z, 0.0f, 0.0f);

    public static Vector3 flatten_0x0(this Vector3 v) => new Vector3(0.0f, v.x, 0.0f);

    public static Vector3 flatten_0y0(this Vector3 v) => new Vector3(0.0f, v.y, 0.0f);

    public static Vector3 flatten_0z0(this Vector3 v) => new Vector3(0.0f, v.z, 0.0f);

    public static Vector3 flatten_00x(this Vector3 v) => new Vector3(0.0f, 0.0f, v.x);

    public static Vector3 flatten_00y(this Vector3 v) => new Vector3(0.0f, 0.0f, v.y);

    public static Vector3 flatten_00z(this Vector3 v) => new Vector3(0.0f, 0.0f, v.z);

    // operations

    public static Vector3 Multiply(this Vector3 a, Vector3 b)
    => new Vector3(
        a.x * b.x,
        a.y * b.y,
        a.z * b.z
    );
}