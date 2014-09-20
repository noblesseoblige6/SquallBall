using UnityEngine;
using System.Collections;

public class GUIRetry : MonoBehaviour
{
		void OnGUI ()
		{

				if (Event.current.type == EventType.MouseDown) {
						Application.LoadLevel ("SquallBall");
				}
		}
}
