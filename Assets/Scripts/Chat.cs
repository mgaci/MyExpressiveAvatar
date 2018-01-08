using UnityEngine;
using System.Collections.Generic;

public class Chat : MonoBehaviour {
	Animator anim; //change
	public static string ranExpression = string.Empty; //change
	public static string[] expressionsList = {"Agreement_weak", "Agreement_medium", "Agreement_strong", "Disagreement_weak", "Disagreement_medium", "Disagreement_strong", "Laughter_weak", "Laughter_medium", "Laughter_strong", "Confusion_weak", "Confusion_medium", "Confusion_strong", "Happiness_weak", "Happiness_medium", "Happiness_strong", "Sadness_weak", "Sadness_medium", "Sadness_strong", "Anger_weak", "Anger_medium", "Anger_strong", "Interest_weak", "Interest_medium", "Interest_strong", "Surprise_weak", "Surprise_medium", "Surprise_strong"};//change
	


	public List<string> chatHistory = new List<string> ();

	public GUISkin skin;

	private string currentMsg = string.Empty;

	private Vector2 scrollPosition;

	public bool trigger = false; 

	public int  count=0;
	private void SendWindow(){
		currentMsg = GUI.TextField (new Rect(10, (Screen.height * 0.75f) + 10, (Screen.width * 0.7f), 20), currentMsg);
		if(GUI.Button(new Rect((Screen.width * 0.7f) + 15, (Screen.height * 0.75f) + 10, (Screen.width * 0.2f), 20), "Send"))
			SendMessage ();

	}

	private void ChatWindow(){
		for(int i= chatHistory.Count-1; i>=0; i--){
			GUILayout.Label(chatHistory[i]);
		}
	}

	private void SendMessage(){
				anim = GetComponent<Animator> (); //change

		foreach(string expression in expressionsList){//change
			anim.SetBool(expression, false); //change
		} //change

		if(!string.IsNullOrEmpty(currentMsg.Trim())){
			GetComponent<NetworkView>().RPC("ChatMessage", RPCMode.AllBuffered, new object[] { NetworkMenu.user + ": " + currentMsg });
			count = count + 1 ;
			print ("count: " + count);

			ranExpression = expressionsList[new System.Random().Next(0, expressionsList.Length-1)]; //change
			print ("Random expression: " + ranExpression); //change
			
			anim.SetBool(ranExpression, true);

			currentMsg = string.Empty;
		}

		currentMsg = string.Empty;
	}


	private void DummyDataGenerator(){
		
	} 
	
	private void OnGUI(){
		if (!NetworkMenu.Connected)
			return;

		GUI.skin = skin;

		GUILayout.BeginHorizontal (GUILayout.Width (Screen.width), GUILayout.Height(Screen.height * 0.6f));

			GUILayout.BeginVertical (GUILayout.Width (Screen.width * 0.7f), GUILayout.Height(Screen.height * 0.6f));
			// avatar
			// dummy data generator
				//DummyDataGenerator ();
			GUILayout.EndVertical ();

			GUILayout.BeginVertical (GUILayout.Width (Screen.width * 0.8f), GUILayout.Height(Screen.height * 0.6f));
				if(chatHistory.Count != 0){
					string lastMsg = chatHistory[chatHistory.Count - 1];
					
					string[] msg = lastMsg.Split(new string[] {" "}, System.StringSplitOptions.None);  					
					
					string tempUser = NetworkMenu.user + ":";
					
					if(!tempUser.Equals(msg[0]) || string.IsNullOrEmpty(tempUser)){
						List<string> msgList = new List<string>(msg);
						msgList.RemoveAt(0);
						
						string finalMsg = "";
						foreach(string message in msgList){
							if(!string.IsNullOrEmpty(finalMsg.Trim()))
								finalMsg = string.Concat(finalMsg, " ", message);
							else
								finalMsg = message;
						}
						
						GUI.Label(new Rect((Screen.width * 0.85f) - 50, (Screen.height * 0.3f) - 50, 100, 100), finalMsg);
					}
				}
			GUILayout.EndVertical ();

		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal (GUILayout.Width (Screen.width), GUILayout.Height(Screen.height * 0.1f));
			SendWindow ();	
		GUILayout.EndHorizontal ();

		GUILayout.BeginArea (new Rect(0, (Screen.height * 0.8f) + 10, Screen.width, (Screen.height * 0.3f) - 30));
		scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUILayout.Width(Screen.width * 0.7f), GUILayout.Height(Screen.height * 0.2f));	
			ChatWindow ();	
		GUILayout.EndScrollView ();
		GUILayout.EndArea ();
	}

	[RPC]
	public void ChatMessage(string msg){
		chatHistory.Add (msg);
	}
}
