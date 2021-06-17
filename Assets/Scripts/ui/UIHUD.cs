using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Match_Invaders.UI
{
	public class UIHUD : MonoBehaviour, IHUDUpdater
	{
		public int PlayerHP { set => throw new System.NotImplementedException(); }
		public int CurrentLevel { set => throw new System.NotImplementedException(); }
		public int CurrentScore { set => throw new System.NotImplementedException(); }
	}
}
