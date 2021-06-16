using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Match_Invaders.Logic
{
	[CreateAssetMenu(fileName = "BattleConfig", menuName = "Match Invaders/BattleConfig")]
	public class BattleConfiguration : ScriptableObject
	{
		// prefabs
		public PlayerShipBehaviour PlayerShipPrefab; 
		public Projectile PlayerProjectilePrefab;
		public EnemyShipBehaviour EnemyShipPrefab;
		public Projectile EnemyProjectilePrefab;
		public Protector ProtectorPrefab;
		public Explosion ExplosionPrefab;

		// player stats section
		[Range(1, 3)]
		public int PlayerHP = 3;
		[Range(0f, 100f)]
		public float MinPlayerShootDelay = 0.5f;
		[Range(0.001f, 100f)]
		public float PlayerProjectileSpeed;
		[Range(1, 100)]
		public int MaxActivePlayerProjectiles = 1;
		[Range(1, 100)]
		public int PlayerProjectileDamage = 1;

		// protector stats section
		[Range(1, 100)]
		public int ProtectorHP = 5;
		[Range(0, 100)]
		public int ProtectorCount = 4;
		
		// battlefield size
		[Range(1f, 100f)]
		public float BattlefieldWidth = 20;
		[Range(1f, 100f)]
		public float BattlefieldHeight = 20;
		

		// fleet size/shape
		[Range(1, 100)]
		public int FleetRows = 6;
		[Range(1, 100)]
		public int FleetColumns = 12;
		[Range(0f, 100f)]
		public float FleetFormationInterval = 2;
		
		// Enemy stats
		[Range(1, 100)]
		public int EnemyHP = 1;
		[Range(1, 100)]
		public int EnemyProjectileDamage = 1;
		[Range(0f, 100f)]
		public float PlayerShipSpeed = 3;
		[Range(0f, 100f)]
		public float EnemyTopSpeed = 1;
		[Range(0f, 100f)]
		public float MinEnemyShootDelay = 0.5f;
		[Range(0f, 100f)]
		public float MaxEnemyShootDelay = 2f;
		[Range(0.001f, 100f)]
		public float EnemyProjectileSpeed = 2;
		[Range(1, 100)]
		public int MaxActiveEnemyProjectiles = 5;
	}
}