using UnityEngine;
using System.Collections;
using System;

public class NetworkService {
	private const string webImage = "http://upload.wikimedia.org/wikipedia/commons/c/c5/Moraine_Lake_17092005.jpg";
	private const string localApi = "http://localhost/ch9/api.php";

	// weather api http://openweathermap.org/api
	private const string jsonApi = "http://api.openweathermap.org/data/2.5/weather?q=Chicago,us";
	private const string xmlApi = "http://api.openweathermap.org/data/2.5/weather?q=Chicago,us&mode=xml";

	private bool IsResponseValid(WWW www) {
		if (www.error != null) {
			Debug.Log("bad connection");
			return false;
		}
		else if (string.IsNullOrEmpty(www.text)) {
			Debug.Log("bad data");
			return false;
		}
		else {	// all good
			return true;
		}
	}

	private IEnumerator CallAPI(string url, Hashtable args, Action<string> callback) {
		WWW www;

		if (args == null) {
			www = new WWW(url);
		} else {
			WWWForm form = new WWWForm();
			foreach(DictionaryEntry arg in args) {
				form.AddField(arg.Key.ToString(), arg.Value.ToString());
			}
			www = new WWW(url, form);
		}

		yield return www;
		
		if (!IsResponseValid(www))
			yield break;
		
		callback(www.text);
	}

	public IEnumerator GetWeatherXML(Action<string> callback) {
		return CallAPI(xmlApi, null, callback);
	}
	public IEnumerator GetWeatherJSON(Action<string> callback) {
		return CallAPI(jsonApi, null, callback);
	}

	public IEnumerator LogWeather(string name, float cloudValue, Action<string> callback) {
		Hashtable args = new Hashtable();
		args.Add("message", name);
		args.Add("cloud_value", cloudValue);
		args.Add("timestamp", DateTime.UtcNow.Ticks);

		return CallAPI(localApi, args, callback);
	}

	public IEnumerator DownloadImage(Action<Texture2D> callback) {
		WWW www = new WWW(webImage);
		yield return www;
		callback(www.texture);
	}
}
