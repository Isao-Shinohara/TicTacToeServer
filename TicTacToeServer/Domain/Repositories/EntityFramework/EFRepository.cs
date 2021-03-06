﻿using TicTacToeServer.Domain.Entitys;
using TicTacToeServer.Domain.Infrastructures;

namespace TicTacToeServer.Domain.Repositorys.EntityFramework
{
	public class EFRepository<T> where T : Entity
	{
		protected readonly EFContext efContext;

		public EFRepository(EFContext efContext)
		{
			this.efContext = efContext;
		}

		public void Save(T entiry)
		{
			efContext.SaveChanges();
		}

		public void Remove(T entiry)
		{
			efContext.Remove(entiry);
		}
	}
}
