using UnityEngine;
using System.Collections;

public class GUIStart : MonoBehaviour
{
		void OnGUI ()
		{
				if (Event.current.type == EventType.MouseDown) {
						Application.LoadLevel ("SquallBall");	
				}
		}
}