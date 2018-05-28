using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//renderer 扩展
public static class RendererExtensions {
	//是否在摄像机内
	public static bool isVisibleExt(this Renderer renderer, Camera camera){
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}
}
