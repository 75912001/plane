using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameAnimation : MonoBehaviour
{
    public List<GameObject> spriteRendererList;
    public float speed;
    private SpriteRenderer spriteRenderer;
    private int idx = 0;
    public void Clear()
    {
        this.spriteRendererList.Clear();
        this.idx = 0;
    }
    public void Add(GameObject spriteRenderer)
    {
        this.spriteRendererList.Add(spriteRenderer);
    }
	// Use this for initialization
	void Start () {
        this.spriteRendererList = new List<GameObject>();
        this.speed = 1.0f;
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();



   
        
        //SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
/*
        Texture2D texture2d36 = (Texture2D)Resources.Load("Prefabs/Plane/p_09d_36");
        Texture2D texture2d37 = (Texture2D)Resources.Load("Prefabs/Plane/p_09d_37");
        Texture2D texture2d40 = (Texture2D)Resources.Load("Prefabs/Plane/p_09d_40");
        Texture2D texture2d41 = (Texture2D)Resources.Load("Prefabs/Plane/p_09d_41");

        Sprite sp36 = Sprite.Create(texture2d36, this.spriteRenderer.sprite.textureRect, new Vector2(0.5f, 0.5f));//注意居中显示采用0.5f值  //创建一个精灵(图片，纹理，二维浮点型坐标)
        Sprite sp37 = Sprite.Create(texture2d37, this.spriteRenderer.sprite.textureRect, new Vector2(0.5f, 0.5f));//注意居中显示采用0.5f值  //创建一个精灵(图片，纹理，二维浮点型坐标)
        Sprite sp40 = Sprite.Create(texture2d40, this.spriteRenderer.sprite.textureRect, new Vector2(0.5f, 0.5f));//注意居中显示采用0.5f值  //创建一个精灵(图片，纹理，二维浮点型坐标)
        Sprite sp41 = Sprite.Create(texture2d41, this.spriteRenderer.sprite.textureRect, new Vector2(0.5f, 0.5f));//注意居中显示采用0.5f值  //创建一个精灵(图片，纹理，二维浮点型坐标)
        this.Add(sp36);
        this.Add(sp37);
        this.Add(sp40);
        this.Add(sp41);
 */
	}
	
	// Update is called once per frame
	void Update () {
        if (0 == this.spriteRendererList.Count)
        {
            return;
        }
        /*
        this.spriteRenderer = this.spriteRendererList[this.idx];
        this.idx++;
        if (this.spriteRendererList.Count == this.idx)
        {
            this.idx = 0;
        }
         *  */
        // int index = (Time.time * this.speed);
       // this.spriteRenderer.sprite = ;
	}
}
