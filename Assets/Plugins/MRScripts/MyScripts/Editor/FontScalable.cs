using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class FontScalable : MonoBehaviour {

	[Range(1, 6)]
	public float fontScale = 1;
	TextMesh textMesh;

	void Start () {
		textMesh = GetComponent<TextMesh>();
	}
	
	void Update () 
	{
		int fontSize = Mathf.Max(12, textMesh.fontSize);
		textMesh.fontSize = fontSize;
		float scale = 0.1f * 128 / fontSize;
		textMesh.characterSize = scale * fontScale;
	}
}