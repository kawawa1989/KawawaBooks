using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TagDropdown : MonoBehaviour {
	[SerializeField]
	Dropdown dropdown;

	public string value {
		get {
			return dropdown.options[dropdown.value].text;
		}
	}

	void Start () {
		var books = Document.instance.directoryPaths;
		var tags = new List<BookTag> ();
		books.ForEach ((x) => tags.AddRange (x.tags));

		var options = new List<Dropdown.OptionData> ();
		foreach (var tag in tags.Select ((x) => x.tagValue).Distinct ().ToList ()) {
			options.Add (new Dropdown.OptionData (tag));
		}
		dropdown.AddOptions (options);
	}
}