using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Indicates that this component is added to a GameObject at runtime.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class AddedAtRuntimeAttribute : Attribute
{
	
}
