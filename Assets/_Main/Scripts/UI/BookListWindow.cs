using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BookListWindow : MonoBehaviour {
	[SerializeField]
	BookPanelUI bookPanelObject;
	[SerializeField]
	Transform booksRoot;

	List<Texture2D> thumbnailTextures = new List<Texture2D> ();
	[SerializeField]
	BookInfoWindow windowObject;
	[SerializeField]
	Transform bookInfoRoot;

	public void Show (List<BookData> books) {
		for (int i = 0; i < books.Count; ++i) {
			var data = books[i];
			var ui = Instantiate (bookPanelObject, booksRoot);
			var thumbnail = LoadThumbnail (data);
			ui.Set (data, thumbnail);
			ui.gameObject.SetActive (true);
			ui.transform.Find ("Button").GetComponent<Button> ().onClick.AddListener (() => {
				var window = Instantiate (windowObject, bookInfoRoot, false);
				window.transform.Find ("Close").GetComponent<Button> ().onClick.AddListener (() => {
					window.Close ();
				});
				window.gameObject.SetActive (true);
				window.Show (data, thumbnail, ui.titleText);
			});
		}
	}

	Texture2D LoadThumbnail (BookData data) {
		var dir = new System.IO.DirectoryInfo (data.path);
		var firstImageFile = dir.GetFiles ().Where ((x) => {
			if (x.Extension == ".jpg") {
				return true;
			}
			if (x.Extension == ".jpeg") {
				return true;
			}
			if (x.Extension == ".png") {
				return true;
			}
			return false;
		}).FirstOrDefault ();

		if (firstImageFile != null) {
			var bytes = System.IO.File.ReadAllBytes (firstImageFile.FullName);
			var tex = new Texture2D (0, 0);
			tex.LoadImage (bytes);
			thumbnailTextures.Add (tex);
			return tex;
		}
		return null;
	}

	public void Close () {
		for (int i = 0; i < thumbnailTextures.Count; ++i) {
			Destroy (thumbnailTextures[i]);
		}
		Destroy (gameObject);
	}
}