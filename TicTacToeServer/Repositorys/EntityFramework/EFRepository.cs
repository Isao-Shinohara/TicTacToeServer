﻿using TicTacToeServer.Entitys;
using TicTacToeServer.Infrastructures;

namespace TicTacToeServer.Repositorys
{
	public class EFRepository<T> where T : Entity
	{
		public EFContext efContext;

		public EFRepository(EFContext efContext)
		{
			this.efContext = efContext;
		}

		public void Save()
		{
			efContext.SaveChanges();
		}

		public void Remove(T entiry)
		{
			efContext.Remove(entiry);
		}
	}
}