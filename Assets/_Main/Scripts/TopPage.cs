using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleFileBrowser;
using UnityEngine;
using UnityEngine.UI;

// 概要
// 本を管理することができるアプリです
// 仕様
// このアプリではフォルダを本として扱います。
// 指定したフォルダの直下にある画像ファイル(png、jpg形式)をページとして扱います
// 本として指定したフォルダの中にあるフォルダは無視されます。
public class TopPage : MonoBehaviour {
	[SerializeField]
	Transform canvas;
	[SerializeField]
	BookListWindow windowObject;
	[SerializeField]
	TagDropdown tagDropdownObject;
	[SerializeField]
	Transform dropdownRoot;
	List<TagDropdown> filters = new List<TagDropdown> ();

	// Use this for initialization
	void Start () {
		Document.instance.Initialize ();
	}

	// Update is called once per frame
	void Update () { }

	// 書物を登録します。
	public void EntryBook () {
		SimpleFileBrowser.FileBrowser.ShowLoadDialog ((path) => {
			var currentDirectory = string.Format ("{0}{1}", System.IO.Directory.GetCurrentDirectory (), System.IO.Path.DirectorySeparatorChar);
			var uri1 = new System.Uri (currentDirectory);
			var uri2 = new System.Uri (path);
			var releative = uri1.MakeRelativeUri (uri2);

			Debug.Log ("current:" + currentDirectory);
			Debug.Log ("releative:" + releative.ToString ());
			Document.instance.AddBook (path);
		}, () => { }, true, System.IO.Directory.GetCurrentDirectory ());
	}

	public void OpenBookList () {
		var window = Instantiate (windowObject, canvas, false);
		var filteredBooks = Document.instance.directoryPaths.Where ((x) => MatchTag (x.tags)).ToList ();
		window.Show (filteredBooks);
		window.gameObject.SetActive (true);
	}

	bool MatchTag (List<BookTag> tags) {
		return filters.All ((x) => {
			var tag = tags.Find ((y) => y.tagValue == x.value);
			if (tag != null) {
				return true;
			}
			return false;
		});
	}

	public void AddTagFilter () {
		var dropdown = Instantiate (tagDropdownObject, dropdownRoot, false);
		filters.Add (dropdown);
		dropdown.gameObject.SetActive (true);
		dropdown.transform.Find ("Button").GetComponent<Button> ().onClick.AddListener (() => {
			filters.Remove (dropdown);
			Destroy (dropdown.gameObject);
		});
	}
}