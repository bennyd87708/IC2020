﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IC2020;

public class Route : MonoBehaviour
{
	[SerializeField]
	public Transform[] controlPoints;

	private Vector2 gizmosPosition;
	
	private void OnDrawGizmos()
	{
		for (float t = 0; t <= 1; t += 0.025f)
		{
			gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position + 3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position + Mathf.Pow(t, 3) * controlPoints[3].position;
			Gizmos.DrawSphere(bezierPosition(t), 0.25f);
		}
		
		//Gizmos.DrawSphere(bezierPosition(0.5f), 0.5f);
		/*
		for(int i = 0; i < controlPoints.Length; i += 2)
		{
			Gizmos.DrawLine(new Vector2(controlPoints[i].position.x, controlPoints[i].position.y), new Vector2(controlPoints[i+1].position.x, controlPoints[i+1].position.y));
		}
		*/
		
	}
	/* spawn a bunch of spheres along the curve
	void Start()
	{
		for (float t = 0; t <= 1; t += 0.005f)
		{
			GameObject curve = new Particle(pos: bezierPosition(t), mass: 0f, bounciness:0f).Spawn();
			curve.GetComponent<Rigidbody>().isKinematic = true;
			curve.GetComponent<MeshRenderer>().enabled = true;
		}
	}
	*/
	
	//slow and inaccurate but it works
	public float nearestPointT(Vector2 pos, float accuracy)
	{
		float nearestT = 0f;
		float nearestDistance = Vector2.Distance(bezierPosition(0f), pos);
		float currentDistance;
		
		for(float t = 0f; t <= 1f; t += accuracy) //increment increases accuracy - need to make less resource intensive - use derivative?
		{
			currentDistance = Vector2.Distance(bezierPosition(t), pos);
			if(currentDistance < nearestDistance)
			{
				nearestDistance = currentDistance;
				nearestT = t;
			}
		}
		return nearestT;
	}
	
	public float newtonsMethod(Vector2 point, float t)
	{
		float currentT = funcD(point, bezierPosition(t));
		float nextT	= t - (currentT / funcDY(point, t));
		if(0.001f > Mathf.Abs(t - nextT))
		{
			return nextT;
		}
		else
		{
			return newtonsMethod(point, nextT);
		}
	}
	/*
	public Vector2 nearestCurvePoint(Vector2 point)
	{
		
	}
	*/
	public float funcD(Vector2 point, Vector2 pos)
	{
		return Mathf.Pow((pos.x - point.x), 2) + Mathf.Pow((pos.y - point.y), 2);
	}
	
	public float funcDY(Vector2 point, float t)
	{
		Vector2 pos = bezierPosition(t);
		Vector2 slope = bezierSlope(t);
		return (2 * pos.x * slope.x) + (2 * pos.y * slope.y);
	}
	
	public Vector2 minFuncP(Vector2 point, Vector2 s1, Vector2 s2, Vector2 s3)
	{
		Vector2 y23 = (Mathf.Pow(s2, 2) - Mathf.Pow(s3, 2)) * funcD(point, s1);
		Vector2 y31 = (Mathf.Pow(s3, 2) - Mathf.Pow(s1, 2)) * funcD(point, s2);
		Vector2 y12 = (Mathf.Pow(s1, 2) - Mathf.Pow(s2, 2)) * funcD(point, s3);
		Vector2 s23 = (s2 - s3) * funcD(point, s1);
		Vector2 s31 = (s3 - s1) * funcD(point, s2);
		Vector2 s12 = (s1 - s2) * funcD(point, s3);
		
		return 0.5f * ((y23 + y31 + y12)/(s23 + s31 + s12));
	}
	
	public Vector2[] quadraticMethod(Vector2 point, Vector2 s1 = bezierPosition(0f), Vector2 s2 = bezierPosition(0.5f), Vector2 s3 = bezierPosition(1f))
	{
		Vector2 star = minFuncP(point, s1, s2, s3);
		Vector2[] newValues = {s1, s2, s3, star};
		float maxDistance = funcP(star, s1, s2, s3);
		for(int i = 0, i < 3; i++)
		{
			
		}
	}
	
	public float funcP(Vector2 point, Vector2 s1, Vector2 s2, Vector2 s3)
	{
		Vector2 first = ((point - s2)*(point - s3)*funcD(point, s1))/((s1 - s2)*(s1 - s3));
		Vector2 second = ((point - s1)*(point - s3)*funcD(point, s2))/((s2 - s1)*(s2 - s3));
		Vector2 third = ((point - s1)*(point - s2)*funcD(point, s3))/((s3 - s1)*(s3 - s2));
		return first + second + third;
	}
	
	public Vector2 bezierPosition(float t)
	{
		Vector2 summation = Vector2.zero;
		int n = controlPoints.Length - 1;
		for(int i = 0; i <= n; i++)
		{
			float binomCoefficient = (factorial(n)/(factorial(i)*factorial(n - i)));
			float mainPower = Mathf.Pow(1 - t, n - i);
			float secondPower = Mathf.Pow(t, i);
			Vector2 point = controlPoints[i].position;
			summation += binomCoefficient * mainPower * secondPower * point;
		}
		return summation;
	}
	
	public Vector2 bezierSlope(float t)
	{
		Vector2 summation = Vector2.zero;
		int n = controlPoints.Length - 1;
		for(int i = 0; i < n; i++)
		{
			float binomCoefficient = (factorial(n-1)/(factorial(i)*factorial(n - (i+1))));
			float mainPower = Mathf.Pow(1 - t, n - (i+1));
			float secondPower = Mathf.Pow(t, i);
			Vector2 point1 = controlPoints[i].position;
			Vector2 point2 = controlPoints[i+1].position;
			summation += binomCoefficient * mainPower * secondPower * n * (point2 - point1);
		}
		return summation;
		//return (summation.y/summation.x); //float version
	}
	
	public static int factorial(int number)
	{
		int val = 1;
		for(int i = 1; i <= number; i++)
		{
			val *= i;
		}
		return val;
	}
}
