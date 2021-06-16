using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Match_Invaders.Logic
{
	class FleetMover
	{
		private BattleConfiguration _config;
		private FleetFormation _fleetFormation;

		public FleetMover(BattleConfiguration config, FleetFormation formation)
		{
			_config = config;
			_fleetFormation = formation;
		}

		enum MoveCycle
		{
			Init = 0,
			MoveRight = 1,
			MoveDown = 2,
			MoveLeft = 3,
		}
		MoveCycle _movementState = MoveCycle.Init;
		private float? _verticalDestination;
		
		private float SpeedWithCurrentFleetSize
		{
			get
			{
				float fleetCount = LiveShips().Count();
				if (0 == fleetCount)
				{
					return 0; // don't bother moving, and avoid potential divzero
				}
				return _config.EnemyTopSpeed / fleetCount;
			}
		}

		public void MaintainMovement()
		{
			if (LiveShips().Count() <= 0)
			{
				return;
			}
			float boundsXOffset = _config.BattlefieldWidth / 2f;
			switch (_movementState)
			{
				case MoveCycle.Init:
					_verticalDestination = null;
					_movementState = MoveCycle.MoveRight; // we might want to do other init stuff here
					break;
				case MoveCycle.MoveRight:
					float rightMostX = RightmostShipX();
					if (rightMostX >= boundsXOffset)
					{
						_movementState = MoveCycle.MoveDown;
					}
					else
					{
						_fleetFormation.Velocity = Vector3.right * SpeedWithCurrentFleetSize;
					}
					break;
				case MoveCycle.MoveLeft:
					float leftMostX = _fleetFormation.Members.Min(o => o.transform.position.x);
					if (leftMostX <= -boundsXOffset)
					{
						_movementState = MoveCycle.MoveDown;
					}
					else
					{
						_fleetFormation.Velocity = Vector3.left * SpeedWithCurrentFleetSize;
					}
					break;
				case MoveCycle.MoveDown:
					if (FleetTooLow())
					{
						AutoChooseMovementDirection(); // just keep cycling left-right immediately without moving down
					}
					if (null == _verticalDestination)
					{
						_verticalDestination = _fleetFormation.transform.position.z - _config.FleetFormationInterval;
					}
					else
					{
						if (LowestShipZ() <= _verticalDestination.Value
							||
							_fleetFormation.transform.position.z <= _verticalDestination.Value
							)
						{
							AutoChooseMovementDirection();
						}
						else
						{
							_fleetFormation.Velocity = Vector3.back * SpeedWithCurrentFleetSize;
						}
					}
					break;
					
			}

		}

		private IEnumerable<EnemyShipBehaviour> LiveShips() => _fleetFormation.Members.Where(o => o.HP > 0);
		
		private float RightmostShipX() => LiveShips().Max(o => o.transform.position.x);
		private float LeftmostShipX() => LiveShips().Min(o => o.transform.position.x);
		private float LowestShipZ() => LiveShips().Min(o => o.transform.position.z);
		private bool FleetTooLow()
		{
			float lowerBoundPlusInterval = _config.FleetFormationInterval + (-_config.BattlefieldHeight / 2f);
			return LowestShipZ() <= lowerBoundPlusInterval;
		}

		private void AutoChooseMovementDirection()
		{
			_verticalDestination = null; // reset this
			float rightBoundDistance = Mathf.Abs(RightmostShipX() - (_config.BattlefieldWidth / 2f));
			float leftBoundDistance = Mathf.Abs(LeftmostShipX() + (_config.BattlefieldWidth / 2f));
			if (leftBoundDistance > rightBoundDistance) // tiebreaker, so no case for equals
			{
				_movementState = MoveCycle.MoveLeft;
			}
			else
			{
				_movementState = MoveCycle.MoveRight;
			}

		}
	}
}
