using System;

struct Vec2f
{
	public float x;
	public float y;

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

	public Vec2f Normalize ()
	{
		return this / Norm ();
	}

	public float Norm ()
	{
		return (float)Math.Sqrt (x * x + y * y);
	}

	public static Vec2f operator / (Vec2f v, float num)
	{
		v.x /= num;
		v.y /= num;

		return v;
	}

	public static Vec2f operator * (Vec2f v, float num)
	{
		v.x *= num;
		v.y *= num;

		return v;
	}

	public static Vec2f operator - (Vec2f a, Vec2f b)
	{
		return new Vec2f { x = a.x - b.x, y = a.y - b.y };
	}

	public static Vec2f operator + (Vec2f a, Vec2f b)
	{
		return new Vec2f { x = a.x + b.x, y = a.y + b.y };
	}
}

public struct Vec3f
{
	public float x;
	public float y;
	public float z;

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

	public Vec3f Normalize ()
	{
		return this / Norm ();
	}

	public float Norm ()
	{
		return (float)Math.Sqrt (x * x + y * y + z * z);
	}

	public static Vec3f operator - (Vec3f a, Vec3f b)
	{
		return new Vec3f { x = a.x - b.x, y = a.y - b.y, z = a.z - b.z };
	}

	public static Vec3f operator / (Vec3f v, float num)
	{
		v.x /= num;
		v.y /= num;
		v.z /= num;

		return v;
	}

	public static Vec3f operator * (Vec3f v, float num)
	{
		v.x *= num;
		v.y *= num;
		v.z *= num;

		return v;
	}
}

/// <summary>
/// a struct that holds multiple floats as a 4 dimensional vector
/// </summary>
struct Vec4f
{
	/// <summary>
	/// float that represents the x value of vector4
	/// </summary>
	public float x;
	/// <summary>
	/// float that represents the y value of vector4
	/// </summary>
	public float y;
	/// <summary>
	/// float that represents the z value of vector4
	/// </summary>
	public float z;
	/// <summary>
	/// float that represents the h value of vector4
	/// </summary>
	public float h;
	
	/// <summary>
	/// property that gets information for the above x,y,z,h variables and then sets them based on the value entered for [int i]
	/// </summary>
	/// <value></value>
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
	
	/// <summary>
	/// function that normalizes the vector4 by taking its length and setting it equal to 1 using the float Norm() function
	/// </summary>
	/// <returns></returns>
	public Vec4f Normalize ()
	{
		var len = Norm ();
		return this / len;
	}
	
	/// <summary>
	/// Function that finds the square root of all 4 of the vector4 values multiplied by themselves and then add to each multiplication result
	/// </summary>
	/// <returns>The normalized value of the vector4</returns>
	public float Norm ()
	{
		return (float)Math.Sqrt (x * x + y * y + z * z + h * h);
	}
	
	/// <summary>
	/// A customized implementation of the operator 'subtract'
	/// </summary>
	/// <param name="a">a single vector4 that is passed into the function when it is called</param>
	/// <param name="b">a single vector4 that is passed into the function when it is called, it is subtracted from parameter 'a'</param>
	/// <returns>a new vector4 that is the difference of a and b</returns>
	public static Vec4f operator - (Vec4f a, Vec4f b)
	{
		return new Vec4f { x = a.x - b.x, y = a.y - b.y, z = a.z - b.z, h = a.h - b.h };
	}
	
	/// <summary>
	/// a customized implementation of the operator 'divide'
	/// </summary>
	/// <param name="v">a vector4 that is passed into the function</param>
	/// <param name="num">a float that the vector4's values are individually divided by</param>
	/// <returns>returns the vector4 v after the divisions</returns>
	public static Vec4f operator / (Vec4f v, float num)
	{
		v.x /= num;
		v.y /= num;
		v.z /= num;
		v.h /= num;

		return v;
	}
}

/// <summary>
/// a struct that creates a 2 dimensional vector using integers
/// </summary>
struct Vec2i
{
	/// <summary>
	/// the x value of the vector2
	/// </summary>
	public int x;
	/// <summary>
	/// the y value of the vector2
	/// </summary>
	public int y;

	/// <summary>
	/// a customized implementation of the operator 'subtract'
	/// </summary>
	/// <param name="a">a vector2 that is passed into the function</param>
	/// <param name="b">a vector2 that is passed into the function and then subtracted from 'a'</param>
	/// <returns>returns a new vector2 that is the difference of a and b</returns>
	public static Vec2i operator - (Vec2i a, Vec2i b)
	{
		return new Vec2i { x = a.x - b.x, y = a.y - b.y };
	}
}

/// <summary>
/// a struct that creates a 3 dimensional vector using integers
/// </summary>
struct Vec3i
{
	/// <summary>
	/// the x value of the vector3
	/// </summary>
	public int x;
	/// <summary>
	/// the y value of the vector3
	/// </summary>
	public int y;
	/// <summary>
	/// the z value of the vector3
	/// </summary>
	public int z;

	/// <summary>
	/// a customized implementation of the operator 'subtract'
	/// </summary>
	/// <param name="a">a vector3 that is passed into the function</param>
	/// <param name="b">a vector3 that is passed into the function and then subtracted from 'a'</param>
	/// <returns>returns a new vector3 that is the difference of a and b</returns>
	public static Vec3i operator - (Vec3i a, Vec3i b)
	{
		return new Vec3i { x = a.x - b.x, y = a.y - b.y, z = a.z - b.z };
	}
}

/// <summary>
/// a static class that contains multiple constructors of the float structs that were created above
/// </summary>
static class Geometry
{
	/// <summary>
	/// A constuctor of the struct Vec3f that is called cross and takes in 2 vector3s
	/// </summary>
	/// <param name="l">vector3 that is passed into the function</param>
	/// <param name="r">vector3 that is passed into the function</param>
	/// <returns>returns a new vector3 that is the product of a</returns>
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