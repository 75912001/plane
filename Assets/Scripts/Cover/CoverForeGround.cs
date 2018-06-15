using UnityEngine;

public class CoverForeGround : MonoBehaviour {

	// Use this for initialization
	void Start () {
        #region 加载封面
        string coverName = "Prefabs/cover_01";
        GameObject prefabs = (GameObject)Resources.Load(coverName);
        if (null == prefabs){
            Debug.LogErrorFormat("Cover未找到{0}", coverName);
        }
        GameObject prefab = Instantiate(prefabs, transform.position, transform.rotation);
        prefab.transform.SetParent(transform);
        #endregion
    }

    // Update is called once per frame
    void Update () {
		
	}
}
