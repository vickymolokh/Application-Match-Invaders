namespace Match_Invaders.Logic
{
	public interface IDamageable
	{
		int HP { get; set; }
		void ApplyDamage(int damage);

		Affiliations GetAffiliation();
	}
}
