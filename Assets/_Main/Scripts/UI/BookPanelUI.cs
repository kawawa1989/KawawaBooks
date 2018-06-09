using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookPanelUI : MonoBehaviour {
	[SerializeField]
	RawImage thumbnailImage;
	[SerializeField]
	Text title;
	public string titleText {
		get {
			return title.text;
		}
	}

	public void Set (BookData data, Texture2D texture) {
		var dinfo = new System.IO.DirectoryInfo (data.path);
		title.text = dinfo.Name;
		thumbnailImage.texture = texture;
	}
}