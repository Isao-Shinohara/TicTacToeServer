using System.Collections.Generic;
using System.Linq;
using TicTacToeServer.Cores;

namespace TicTacToeServer.Entitys
{
	public class RoomEntity : Entity
	{
		public int RoomNumber { get; private set; }
		public RoomType RoomType { get; private set; }
		public TurnType NowTurnType { get; private set; }
		public PlayerEntity _1stPlayer { get; private set; }
		public PlayerEntity _2ndPlayer { get; private set; }
		public List<PanelAreaEntity> PanelAreaList { get; private set; }

		public RoomEntity()
		{
		}

		public RoomEntity(RoomType roomType, PlayerEntity _1stPlayer) : this(99999, roomType, _1stPlayer)
		{
		}

		public RoomEntity(int roomNumber, RoomType roomType, PlayerEntity _1stPlayer)
		{
			RoomNumber = roomNumber;
			RoomType = roomType;
			this._1stPlayer = _1stPlayer;

			NowTurnType = TurnType._1stPlayer;
			PanelAreaList = GetInitialPanelArea();
		}

		public void Set2ndPlayer(PlayerEntity player)
		{
			_2ndPlayer = player;
		}

		public void SelectPanelArea(PlayerEntity player, PanelAreaType panelAreaType)
		{
			var turnType = player == _1stPlayer ? TurnType._1stPlayer : TurnType._2ndPlayer;
			var panel = PanelAreaList.FirstOrDefault(x => x.PanelAreaType == panelAreaType);
			panel.SetTurnType(turnType);
		}

		public void NextTurn()
		{
			switch (NowTurnType)
			{
				case TurnType._1stPlayer:
					NowTurnType = TurnType._2ndPlayer;
					break;
				case TurnType._2ndPlayer:
					NowTurnType = TurnType._1stPlayer;
					break;
			}
		}

		public bool IsClear
		{
			get {
				var existsArea1 = PanelAreaList.Exists(x => x.PanelAreaType == PanelAreaType.Area1);
				var existsArea2 = PanelAreaList.Exists(x => x.PanelAreaType == PanelAreaType.Area2);
				var existsArea3 = PanelAreaList.Exists(x => x.PanelAreaType == PanelAreaType.Area3);
				var existsArea4 = PanelAreaList.Exists(x => x.PanelAreaType == PanelAreaType.Area4);
				var existsArea5 = PanelAreaList.Exists(x => x.PanelAreaType == PanelAreaType.Area5);
				var existsArea6 = PanelAreaList.Exists(x => x.PanelAreaType == PanelAreaType.Area6);
				var existsArea7 = PanelAreaList.Exists(x => x.PanelAreaType == PanelAreaType.Area7);
				var existsArea8 = PanelAreaList.Exists(x => x.PanelAreaType == PanelAreaType.Area8);
				var existsArea9 = PanelAreaList.Exists(x => x.PanelAreaType == PanelAreaType.Area9);

				// Vertical
				var isClear = existsArea1 && existsArea2 && existsArea3;
				if (isClear) return true;
				isClear = existsArea4 && existsArea5 && existsArea6;
				if (isClear) return true;
				isClear = existsArea7 && existsArea8 && existsArea9;
				if (isClear) return true;

				// Horizontal
				isClear = existsArea1 && existsArea4 && existsArea7;
				if (isClear) return true;
				isClear = existsArea2 && existsArea5 && existsArea8;
				if (isClear) return true;
				isClear = existsArea7 && existsArea8 && existsArea9;
				if (isClear) return true;

				// Diagonal
				isClear = existsArea1 && existsArea5 && existsArea9;
				if (isClear) return true;
				isClear = existsArea3 && existsArea5 && existsArea7;
				if (isClear) return true;

				return false;
			}
		}

		public bool IsEnd
		{
			get {
				return PanelAreaList.All(x => x.Selected);
			}
		}

		public ResultType _1stPlayerResult
		{
			get {
				return GetResult(_1stPlayer);
			}
		}

		public ResultType _2ndPlayerResult
		{
			get {
				return GetResult(_2ndPlayer);
			}
		}

		private ResultType GetResult(PlayerEntity player)
		{
			if (!IsEnd) return ResultType.None;

			var turnType = player == _1stPlayer ? TurnType._1stPlayer : TurnType._2ndPlayer;

			if (IsClear){
				if (turnType == NowTurnType) return ResultType.Win;
				if (turnType != NowTurnType) return ResultType.Lose;
			}

			return ResultType.Draw;
		}

		private List<PanelAreaEntity> GetInitialPanelArea()
		{
			return new List<PanelAreaEntity>() {
				new PanelAreaEntity(PanelAreaType.Area1),
				new PanelAreaEntity(PanelAreaType.Area2),
				new PanelAreaEntity(PanelAreaType.Area3),
				new PanelAreaEntity(PanelAreaType.Area4),
				new PanelAreaEntity(PanelAreaType.Area5),
				new PanelAreaEntity(PanelAreaType.Area6),
				new PanelAreaEntity(PanelAreaType.Area7),
				new PanelAreaEntity(PanelAreaType.Area8),
				new PanelAreaEntity(PanelAreaType.Area9),
			};
		}
	}
}
