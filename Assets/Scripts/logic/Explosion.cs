using UnityEngine;
namespace Match_Invaders.Logic
{
	public class Explosion : AbstractSpaceObject<Explosion>
	{
		public delegate void OnExplosionExpiredEvent(Explosion sender);
		public event OnExplosionExpiredEvent OnExpired;
		public float Duration = 1f;
		public float MaxScale = 1f;

		private float _startTime;
		private float _endTime;

		public void BeginExploding()
		{
			transform.localScale = Vector3.one * Mathf.Epsilon;
			_startTime = Time.time;
			_endTime = Time.time + Duration;
			enabled = true;
		}

		public void Update()
		{
			if (Time.time >= _endTime)
			{
				OnExpired?.Invoke(this);
				enabled = false;
				return;
			}
			float lerpValue = Mathf.InverseLerp(_startTime, _endTime, Time.time);
			transform.localScale = Mathf.Lerp(Mathf.Epsilon, MaxScale, lerpValue) * Vector3.one;
		}
	}
}