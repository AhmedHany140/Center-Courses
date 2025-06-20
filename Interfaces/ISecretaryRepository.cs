using CenterDragon.Models.Entites;

namespace CenterDragon.Interfaces
{
	public interface ISecretaryRepository
	{
		Task<Secertary> GetFirstSecretaryAsync();
		Task<Secertary> GetSecretaryBySecurityKeyAsync(string securityKey);
	}
}
