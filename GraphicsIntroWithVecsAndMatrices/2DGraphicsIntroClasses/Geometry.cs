using System;

/// <summary>
/// A struct containing Vec2f data with x and y coordinates
/// </summary>
struct Vec2f
{
    /// <summary>
    /// An x coordinate float variable
    /// </summary>
	public float x;
    /// <summary>
    /// A y coordinate float variable
    /// </summary>
	public float y;

    /// <summary>
    /// Gets or sets the x and y float coordinates based on an array index i depending on if i is empty or full (0 or 1) or null, where it throw invalid operation.
    /// </summary>
    /// <param name="i">An int i in the array index</param>
    /// <returns>The get function returns x if i is 0 or returns y if i is 1</returns>
	public float this [int i] {
		get {
			if (i == 0) return x;
			if (i == 1) return y;
			throw new InvalidOperationException ();
		}
		set {
			if (i == 0) x = value;
			else if (i == 1) y = value;
			else throw new InvalidOperationException ();
		}
	}

    /// <summary>
    /// Divides the current Vec2f by the Norm function (normalizes the current vector)
    /// </summary>
    /// <returns>Returns a Vec2f equal to this function divided by Norm ()</returns>
	public Vec2f Normalize ()
	{
		return this / Norm ();
	}

    /// <summary>
    /// Calculates a new normalized vector which maintains its same direction but has the length of 1
    /// </summary>
    /// <returns>Returns the square root of x squared plus y squared cast as a float</returns>
	public float Norm ()
	{
		return (float)Math.Sqrt (x * x + y * y);
	}

    /// <summary>
    /// Overloads the division operator to set the x and y coordinated of a vector equal to the dividend of float num and return that vector
    /// </summary>
    /// <param name="v">A Vec2f v to be set</param>
    /// <param name="num">A float num to set vector coordinates</param>
    /// <returns>Returns a Vec2f v</returns>
	public static Vec2f operator / (Vec2f v, float num)
	{
		v.x /= num;
		v.y /= num;

		return v;
	}

    /// <summary>
    /// Overloads the * operator to set the x and y coordinates of a vector v to the product of each times a float num
    /// </summary>
    /// <param name="v">A Vec2f v to set the coordinates of</param>
    /// <param name="num">A float num to set x and y coordinates</param>
    /// <returns>Returns a Vec2f v</returns>
	public static Vec2f operator * (Vec2f v, float num)
	{
		v.x *= num;
		v.y *= num;

		return v;
	}

    /// <summary>
    /// Overloads the - operator to return the difference of the coordinates of two vectors a and b
    /// </summary>
    /// <param name="a">A Vec2f a</param>
    /// <param name="b">A Vec2f b</param>
    /// <returns>A new Vec2F with x and y coordinates equal to the difference of the coordinates a and b</returns>
	public static Vec2f operator - (Vec2f a, Vec2f b)
	{
		return new Vec2f { x = a.x - b.x, y = a.y - b.y };
	}

    /// <summary>
    /// Overloads the + operator to return the sum of the coordinates of two vectors a and b
    /// </summary>
    /// <param name="a">A Vec2f a</param>
    /// <param name="b">A vec2f b</param>
    /// <returns>A new Vec2F with x and y coordinates equal to the sum of the coordinates a and b</returns>
	public static Vec2f operator + (Vec2f a, Vec2f b)
	{
		return new Vec2f { x = a.x + b.x, y = a.y + b.y };
	}
}

/// <summary>
/// A Vec3f struct of particular x, y, and z coordinates
/// </summary>
public struct Vec3f
{
	public float x;
	public float y;
	public float z;

    /// <summary>
    /// Gets or sets the float values of x, y, and z coordinates depending on the value of the array index i
    /// </summary>
    /// <param name="i">An int i for index</param>
    /// <returns>Returns float x, y, or z depending on if int i is 0, 1, or 2</returns>
	public float this [int i] {
		get {
			switch (i) {
			case 0: return x;
			case 1: return y;
			case 2: return z;
			default: throw new InvalidOperationException ();
			}
		}
		set {
			switch (i) {
			case 0: x = value; break;
			case 1: y = value; break;
			case 2: z = value; break;
			default: throw new InvalidOperationException ();
			}
		}
	}

    /// <summary>
    /// Divides the current Vec3f by the Norm function (normalizes the current vector)
    /// </summary>
    /// <returns></returns>
	public Vec3f Normalize ()
	{
		return this / Norm ();
	}

    /// <summary>
    /// Calculates a new normalized vector which maintains its same direction but has the length of 1
    /// </summary>
    /// <returns>Returns the square root of x squared plus y squared plus z squared cast as a float</returns>
	public float Norm ()
	{
		return (float)Math.Sqrt (x * x + y * y + z * z);
	}

    /// <summary>
    /// Overloads the - operator to set the x, y, and z coordinates of a vector equal to the difference of float num and return that vector
    /// </summary>
    /// <param name="v">A Vec3f v to be set</param>
    /// <param name="num">A float num to set vector coordinates</param>
    /// <returns>Returns a new Vec3f v</returns>
    public static Vec3f operator - (Vec3f a, Vec3f b)
	{
		return new Vec3f { x = a.x - b.x, y = a.y - b.y, z = a.z - b.z };
	}

    /// <summary>
    /// Overloads the division operator to set the x and y coordinated of a vector equal to the dividend of float num and return that vector
    /// </summary>
    /// <param name="v">A Vec3f v to be set</param>
    /// <param name="num">A float num to set vector coordinates</param>
    /// <returns>Returns a Vec3f v</returns>
    public static Vec3f operator / (Vec3f v, float num)
	{
		v.x /= num;
		v.y /= num;
		v.z /= num;

		return v;
	}

    /// <summary>
    /// Overloads the * operator to set the x and y coordinates of a vector v to the product of each times a float num
    /// </summary>
    /// <param name="v">A Vec3f v to set the coordinates of</param>
    /// <param name="num">A float num to set x and y coordinates</param>
    /// <returns>Returns a Vec3f v</returns>
    public static Vec3f operator * (Vec3f v, float num)
	{
		v.x *= num;
		v.y *= num;
		v.z *= num;

		return v;
	}
}

//Andrew -  Halfway point

struct Vec4f
{
	public float x;
	public float y;
	public float z;
	public float h;

	public float this [int i] {
		get {
			switch (i) {
				case 0: return x;
				case 1: return y;
				case 2: return z;
				case 3: return h;
				default: throw new InvalidOperationException ();
			}
		}
		set {
			switch (i) {
				case 0: x = value; break;
				case 1: y = value; break;
				case 2: z = value; break;
				case 3: h = value; break;
				default: throw new InvalidOperationException ();
			}
		}
	}

	public Vec4f Normalize ()
	{
		var len = Norm ();
		return this / len;
	}

	public float Norm ()
	{
		return (float)Math.Sqrt (x * x + y * y + z * z + h * h);
	}

	public static Vec4f operator - (Vec4f a, Vec4f b)
	{
		return new Vec4f { x = a.x - b.x, y = a.y - b.y, z = a.z - b.z, h = a.h - b.h };
	}

	public static Vec4f operator / (Vec4f v, float num)
	{
		v.x /= num;
		v.y /= num;
		v.z /= num;
		v.h /= num;

		return v;
	}
}

struct Vec2i
{
	public int x;
	public int y;

	public static Vec2i operator - (Vec2i a, Vec2i b)
	{
		return new Vec2i { x = a.x - b.x, y = a.y - b.y };
	}
}

struct Vec3i
{
	public int x;
	public int y;
	public int z;

	public static Vec3i operator - (Vec3i a, Vec3i b)
	{
		return new Vec3i { x = a.x - b.x, y = a.y - b.y, z = a.z - b.z };
	}
}

static class Geometry
{
	public static Vec3f Cross (Vec3f l, Vec3f r)
	{
		return new Vec3f {
			x = l.y * r.z - l.z * r.y,
			y = l.z * r.x - l.x * r.z,
			z = l.x * r.y - l.y * r.x
		};
	}

	public static float Dot (Vec3f l, Vec3f r)
	{
		return l.x * r.x + l.y * r.y + l.z * r.z;
	}

	public static Vec4f Embed4D (Vec3f v, float fill = 1)
	{
		return new Vec4f { x = v.x, y = v.y, z = v.z, h = fill };
	}

	public static Vec2f Project2D (Vec3f v)
	{
		return new Vec2f { x = v.x, y = v.y };
	}

	public static Vec2f Project2D (Vec4f v)
	{
		return new Vec2f { x = v.x, y = v.y };
	}

	public static Vec3f Project3D (Vec4f v)
	{
		return new Vec3f { x = v.x, y = v.y, z = v.z };
	}
}