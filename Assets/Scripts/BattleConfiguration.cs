using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleConfig", menuName = "Match Invaders/BattleConfig")]
public class BattleConfiguration : ScriptableObject
{
	public GameObject PlayerShipPrefab;
	public GameObject PlayerProjectilePrefab;
	public GameObject EnemyShipPrefab;
	public GameObject EnemyProjectilePrefab;
	public GameObject ProtectorPrefab;
	public GameObject ExplosionPrefab;

	[Range(1,3)]
	public int PlayerHP = 3;
	[Range(0f, 100f)]
	public float MinPlayerShootDelay = 0.5f;
	[Range(0.001f, 100f)]
	public float PlayerProjectileSpeed;
	[Range(1,100)]
	public int ProtectorHP = 5;
	[Range(0, 100)]
	public int ProtectorCount = 4;
	[Range(1f, 100f)]
	public float BattlefieldWidth = 20;
	[Range(1f, 100f)]
	public float battlefieldHeight = 20;
	[Range(1,100)]
	public int FleetRows = 6;
	[Range(1,100)]
	public int FleetColumns = 12;
	[Range(0f,100f)]
	public float FleetFormationInterval = 5;
	[Range(1,100)]
	public int EnemyHP = 1;
	[Range(1,100)]
	public int PlayerProjectileDamage = 1;
	[Range(1, 100)]
	public int EnemyProjectileDamage = 1;
	[Range(0f,100f)]
	public float PlayerTopSpeed = 3;
	[Range(0f,100f)]
	public float EnemyTopSpeed = 1;

	[Range(0f, 100f)]
	public float MinEnemyShootDelay = 0.5f;
	[Range(0f, 100f)]
	public float MaxEnemyShootDelay = 2f;

	[Range(0.001f, 100f)]
	public float EnemyProjectileSpeed;
	[Range(0, 100)]
	public int MaxActiveEnemyProjectiles;
}
