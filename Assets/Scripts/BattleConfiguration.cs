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

	public int PlayerHP = 3;
	public int ProtectorHP = 5;
	public int ProtectorCount = 4;
	public Vector2 BattlefieldBounds;
	public float FleetFormationInterval = 5;
	public int EnemyHP = 1;
	public int PlayerProjectileDamage = 1;
	public int EnemyProjectileDamage = 1;
	public float PlayerTopSpeed = 3;
	public float EnemyTopSpeed = 1;

	
}
