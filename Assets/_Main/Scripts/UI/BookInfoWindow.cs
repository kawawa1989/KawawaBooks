using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookInfoWindow : MonoBehaviour {
	[SerializeField]
	GameObject tagObject;
	[SerializeField]
	Transform tagRoot;
	[SerializeField]
	RawImage thumbnailImage;
	[SerializeField]
	Text titleText;

	[SerializeField]
	GameObject tagEditWindow;
	BookData data;

	public void Show (BookData data, Texture2D thumbnail, string title) {
		titleText.text = title;
		thumbnailImage.texture = thumbnail;
		this.data = data;
		for (int i = 0; i < data.tags.Count; ++i) {
			CreateTag (data.tags[i]);
		}
	}

	public void CreateTag (BookTag tagData) {
		var tag = Instantiate (tagObject, tagRoot);
		tag.SetActive (true);
		tag.transform.Find ("Text").GetComponent<Text> ().text = tagData.tagValue;
		tag.transform.Find ("Button").GetComponent<Button> ().onClick.AddListener (() => {
			data.tags.Remove (tagData);
			Destroy (tag);
		});
	}

	public void AddTag () {
		tagEditWindow.SetActive (true);
	}

	public void Open () {
		var dir = new System.IO.DirectoryInfo (data.path);
		Debug.Log ("Open " + dir.FullName);
		System.Diagnostics.Process.Start (dir.FullName);
	}

	public void CloseTagEdit () {
		tagEditWindow.SetActive (false);
	}

	public void DecideTag (string text) {
		var tag = new BookTag ();
		tag.tagValue = text;
		data.tags.Add (tag);
		CreateTag (tag);
		CloseTagEdit ();
	}

	public void Close () {
		Document.Save ();
		Destroy (gameObject);
	}
}