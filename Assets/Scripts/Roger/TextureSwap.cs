// Change renderer's texture each changeInterval/
// seconds from the texture array defined in the inspector.

using UnityEngine;
using System.Collections;

public class TextureSwap : MonoBehaviour
{
	private float duration = 0.2f;
	private bool changeTextureOK = false;
	public Texture[] textures;
	Renderer rend;

	void Start()
	{
		rend = GetComponent<Renderer>();
	}

	public void changeTexture()
	{
		StartCoroutine(DoTextureLoop());
		//	changeTextureOK = true;
		//	if (changeTextureOK)
		//	{
		//Update();
		//	enabled = true;
		//	}
		//	//for (int i = 1; i < textures.Length; i++)
		//	//{
		//	//	rend.material.mainTexture = textures[i];
		//	//	StartCoroutine(Wait());
		//	//	print("Texture on position" + i);
		//	//}
	}

	public IEnumerator DoTextureLoop()
	{
		changeTextureOK = true;
		print(changeTextureOK);
		for (int i = 1; i < textures.Length; i++)
		{
			if (changeTextureOK)
			{
				rend.material.mainTexture = textures[i];
				print("Texture position" + i);
				yield return new WaitForSeconds(duration);
			}
		}
		changeTextureBack();
	}

	//public IEnumerator Wait()
	//{
	//	yield return new WaitForSeconds(8.0f);
	//}

	public void changeTextureBack()
    {
		changeTextureOK = false;
		rend.material.mainTexture = textures[0];
	}

 //   void FixedUpdate()
 //   {
	//		if (textures.Length == 0)
	//			return;

	//		int index = Mathf.FloorToInt(Time.time / changeInterval);
	//		index = index % textures.Length;
	//		rend.material.mainTexture = textures[index];
	//}
}