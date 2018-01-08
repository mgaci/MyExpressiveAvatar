using UnityEngine;
using System.Collections;

public class NetworkMenu : MonoBehaviour {
	public string connectionIP = "127.0.0.1";
	public int portNumber = 8632;
	public static bool Connected { get; private set; }
	public static string user = string.Empty;

	private void OnConnectedToServer(){
		// A client has just connected
		Connected = true;
	}

	private void OnServerInitialized(){
		// The server has initialised
		Connected = true;
	}

	private void OnDisconnectedFromServer(){
		// The connection has been lost or disconnected
		Connected = false;
	}

	private void OnGUI(){
		if (!Connected) {
			connectionIP = GUILayout.TextField (connectionIP);
			int.TryParse (GUILayout.TextField (portNumber.ToString ()), out portNumber);

			if (GUILayout.Button ("Connect"))
				Network.Connect (connectionIP, portNumber);

			if (GUILayout.Button ("Host"))
				Network.InitializeServer (4, portNumber, true); 
		} else {
			GUILayout.Label ("Connections: " + Network.connections.Length.ToString ());
			GUILayout.Label ("User: ");
			user = GUILayout.TextField(user);
		}
	}
}
