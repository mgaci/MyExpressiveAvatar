using UnityEngine;
using System.Collections;

public class AnimatorController : MonoBehaviour {
	Animator anim;
	public static string ranExpression = string.Empty;
	public static string[] expressionsList = {"Agreement_weak", "Agreement_medium", "Agreement_strong", "Disagreement_weak", "Disagreement_medium", "Disagreement_strong", "Laughter_weak", "Laughter_medium", "Laughter_strong", "Confusion_weak", "Confusion_medium", "Confusion_strong", "Happiness_weak", "Happiness_medium", "Happiness_strong", "Sadness_weak", "Sadness_medium", "Sadness_strong", "Anger_weak", "Anger_medium", "Anger_strong", "Interest_weak", "Interest_medium", "Interest_strong", "Surprise_weak", "Surprise_medium", "Surprise_strong"};


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();

		ranExpression = expressionsList[new System.Random().Next(0, expressionsList.Length-1)];
		print ("Random expression: " + ranExpression);

		foreach(string expression in expressionsList){
			anim.SetBool(expression, false);

		}
	}
	
	// Update is called once per frame
	void Update () {
		foreach(string expression in expressionsList){
			if(expression.Equals(ranExpression)){
				anim.SetBool(expression, true);
			}
		}
	}
}
