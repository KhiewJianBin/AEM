using System.Collections.Generic;
using com.eliotlash.core.util;
using UnityEngine;

public class test : MonoBehaviour
{
    public enum fruits { apple = 1,orange=2,pear=4,a,b,c,d,e}
    [EnumFlag]
    public fruits c;

	// Use this for initialization
	void Start ()
	{
		//int a = c&fruits.apple;
	}
	
	void Update ()
	{

	}
}
