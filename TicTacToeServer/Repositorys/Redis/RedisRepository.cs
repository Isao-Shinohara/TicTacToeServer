using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using TicTacToeServer.Entitys;

namespace TicTacToeServer.Repositorys.Redis
{
	public class RedisRepository<T> where T : Entity
	{
		protected  IDistributedCache cache;

		public RedisRepository(IDistributedCache cache)
		{
			this.cache = cache;
		}

		public void Save()
		{
		}

		public void Remove(T entiry)
		{
		}

		//public string Get()
		//{
		//	var cacheKey = "TheTime";
		//	var existingTime = cache.GetString(cacheKey);
		//	if (!string.IsNullOrEmpty(existingTime)) {
		//		return "Fetched from cache : " + existingTime;
		//	} else {
		//		existingTime = DateTime.UtcNow.ToString();
		//		cache.SetString(cacheKey, existingTime);
		//		return "Added to cache : " + existingTime;
		//	}
		//}
	}
}
