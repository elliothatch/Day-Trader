using UnityEngine;
using System.Collections;

public class ObjectEraseTexture : MonoBehaviour {

	public OTSprite eraserObject;
	public Texture2D targetTexture;
	private OTSprite sprite;
	private Texture2D baseTex;
	private Vector2? lastPos = null;
	// Use this for initialization
	void Start () {
		sprite = GetComponent<OTSprite>();
		sprite.onStay = OnStay;
		reset();
	}
	
	void reset()
	{
		baseTex = (Texture2D)Instantiate(targetTexture);
		baseTex.Apply();
		sprite.image = baseTex;
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0))
			lastPos = null;
		//if(Input.GetMouseButton(1))
			//reset();
	}

	void OnStay(OTObject owner)
	{
		
		if(Input.GetMouseButton(0) && owner.collisionObject == eraserObject)
		{
			Vector2 curPos = owner.collisionObject.position - sprite.position + sprite.size/2;
			if(!lastPos.HasValue)
				lastPos = curPos + new Vector2(1,1);
			PaintLine (curPos, lastPos.GetValueOrDefault(),
				10.0f,new Color(0,0,0,0),10.0f,baseTex);
			baseTex.Apply ();
			sprite.image = baseTex;
			lastPos = curPos;
   		}
	}


	static Texture2D  PaintLine ( Vector2 from ,  Vector2 to ,  float rad ,  Color col ,  float hardness ,  Texture2D tex  )
{
        //var width = rad*2;
 
    var extent = rad;
    var stY = Mathf.Clamp(Mathf.Min(from.y, to.y) - extent, 0, tex.height);
    var stX = Mathf.Clamp(Mathf.Min(from.x, to.x) - extent, 0, tex.width);
    var endY = Mathf.Clamp(Mathf.Max(from.y, to.y) + extent, 0, tex.height);
    var endX = Mathf.Clamp(Mathf.Max(from.x, to.x) + extent, 0, tex.width);
 
    var lengthX = endX - stX;
    var lengthY = endY - stY;
 
    //var sqrRad = rad * rad;
    var sqrRad2 = (rad + 1) * (rad + 1);
    Color[] pixels = tex.GetPixels((int)stX, (int)stY, (int)lengthX, (int)lengthY, 0);
    var start = new Vector2(stX, stY);
        //Debug.Log (widthX + "   "+ widthY + "   "+ widthX*widthY);
        for (int y=0;y<(int)lengthY;y++)
    {
                for (int x=0;x<(int)lengthX;x++)
        {
                        var p = new Vector2 (x,y) + start;
            var center = p + new Vector2(0.5f, 0.5f);
                        float dist = (center-NearestPointStrict(from,to,center)).sqrMagnitude;
                        if (dist>sqrRad2) {
                                continue;
                        }
                        dist = GaussFalloff (Mathf.Sqrt(dist),rad) * hardness;
                        //dist = (samples[i]-pos).sqrMagnitude;
            Color c;
                        if (dist>0) {
                c = Color.Lerp(pixels[y * (int)lengthX + x], col, dist);
                        } else {
                c = pixels[y * (int)lengthX + x];
                        }
 
            pixels[y * (int)lengthX + x] = c;
                }
        }
    tex.SetPixels((int)start.x, (int)start.y, (int)lengthX, (int)lengthY, pixels, 0);
        return tex;
}

static Vector3 NearestPoint ( Vector3 lineStart ,   Vector3 lineEnd ,   Vector3 point  ){
        var lineDirection= Vector3.Normalize(lineEnd-lineStart);
        var closestPoint= Vector3.Dot((point-lineStart),lineDirection)/Vector3.Dot(lineDirection,lineDirection);
        return lineStart+(closestPoint*lineDirection);
}
 
static Vector3 NearestPointStrict ( Vector3 lineStart ,   Vector3 lineEnd ,   Vector3 point  ){
        var fullDirection= lineEnd-lineStart;
        var lineDirection= Vector3.Normalize(fullDirection);
        var closestPoint= Vector3.Dot((point-lineStart),lineDirection)/Vector3.Dot(lineDirection,lineDirection);
        return lineStart+(Mathf.Clamp(closestPoint,0.0f,Vector3.Magnitude(fullDirection))*lineDirection);
}
 
static Vector2 NearestPointStrict ( Vector2 lineStart ,   Vector2 lineEnd ,   Vector2 point  ){
        var fullDirection= lineEnd-lineStart;
        var lineDirection= Normalize(fullDirection);
        var closestPoint= Vector2.Dot((point-lineStart),lineDirection)/Vector2.Dot(lineDirection,lineDirection);
        return lineStart+(Mathf.Clamp(closestPoint,0.0f,fullDirection.magnitude)*lineDirection);
}
static float GaussFalloff ( float distance  ,   float inRadius  ){
        return Mathf.Clamp01 (Mathf.Pow (360.0f, -Mathf.Pow (distance / inRadius, 2.5f) - 0.01f));
}
static Vector2 Normalize(Vector2 p)
{
        float mag = p.magnitude;
        return p/mag;
}
}
