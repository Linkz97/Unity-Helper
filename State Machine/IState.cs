using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace LinkzJ.Games.Animations
{
	public interface IState
	{
		public bool isCompleteded { get; set; }
		public void OnEnter();
		public void Update();
		public void OnExit();
		public void FixedUpdate();
	}
}