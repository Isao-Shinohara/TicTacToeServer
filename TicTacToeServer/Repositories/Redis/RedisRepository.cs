using System;
using System.Threading.Tasks;
using MessagePack;
using Microsoft.Extensions.Caching.Distributed;
using TicTacToeServer.Cores;
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

		protected void SetPrimaryId(T eintity)
		{
			var primaryId = 1;

			var key = string.Format("{0}:primaryid", typeof(T).Name);
			var id = cache.GetString(key);
			if(id != null){
				primaryId = int.Parse(id);
			}
			eintity.SetId(primaryId);

			primaryId++;
			cache.SetString(key, primaryId.ToString());
		}

		protected T Get(string key, object id)
		{
			var bytes = cache.Get(ConvertToRedisKey(key, id));
			if (bytes == null) return null;
			return MessagePackSerializer.Deserialize<T>(bytes);
		}

		protected void Set(string key, object id, T entity)
		{
			var bytes = MessagePackSerializer.Serialize(entity);
			cache.Set(ConvertToRedisKey(key, id), bytes);
		}

		protected string GetString(string key, object id)
		{
			return cache.GetString(ConvertToRedisKey(key, id));
		}

		protected void SetString(string key, object id, object value)
		{
			cache.SetString(ConvertToRedisKey(key, id), value.ToString());
		}

		protected void Remove(string key, object id)
		{
			cache.Remove(ConvertToRedisKey(key, id));
		}

		protected string ConvertToRedisKey(string key, object id)
		{
			return string.Format("{0}:{1}:{2}", typeof(T).Name.ToLower(), key.ToLower(), id);
		}
	}
}
