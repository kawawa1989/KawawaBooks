using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class BookTag {
	public string tagValue = null;
}

[Serializable]
public class BookData {
	// 書物のパス
	public string path = null;

	// 書物に関連付けされたタグ
	public List<BookTag> tags = new List<BookTag> ();

	// 閲覧回数
	// ディレクトリにアクセスした段階で１カウントします
	public int watchCount = 0;
	public string bookName;
}

// アプリ内で使われる情報を保持します
[Serializable]
public class Document {
	const string FileName = "booksdata.json";
	public List<BookData> directoryPaths = new List<BookData> ();

	public void Initialize () { }

	public static void Save () {

		string json = UnityEngine.JsonUtility.ToJson (instance);
		System.IO.File.WriteAllText (FileName, json);
	}

	static Document Load () {
		if (!System.IO.File.Exists (FileName)) {
			return null;
		}
		string json = System.IO.File.ReadAllText (FileName);
		if (string.IsNullOrEmpty (json)) {
			return null;
		}
		return UnityEngine.JsonUtility.FromJson<Document> (json);
	}

	public BookData AddBook (string bookName, string path) {
		var book = directoryPaths.Find ((x) => x.path == path);
		if (book != null) {
			return book;
		}
		var newBook = new BookData ();
		newBook.bookName = bookName;
		newBook.path = path;
		directoryPaths.Add (newBook);
		UnityEngine.Debug.Log ("登録 path:" + path);
		Save ();
		return newBook;
	}

	public bool ContainsBook(string bookName){
		var bookData = directoryPaths.Find((x)=>x.bookName == bookName);
		if (bookData != null) {
			return true;
		}
		return false;
	}

	[NonSerialized]
	static Document _instance;

	public static Document instance {
		get {
			if (_instance == null) {
				_instance = Load ();
				if (_instance == null) {
					_instance = new Document ();
				} else {
					UnityEngine.Debug.Log("Load!");
				}
			}
			return _instance;
		}
	}
}