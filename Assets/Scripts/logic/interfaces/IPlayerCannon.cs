using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_Invaders.Logic
{
	public interface IPlayerCannon
	{
		void TryShoot();
		void DestroyAllPoolObjects(); 
	}
}
