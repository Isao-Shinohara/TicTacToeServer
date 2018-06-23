﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using MessagePack;
using TicTacToeServer.Core;

namespace TicTacToeServer.Domain.Entitys
{
	[MessagePackObject]
	public class RoomEntity : Entity
	{
		[Key(0)]
		public int RoomId { get; private set; }
		[Key(1)]
		public int RoomNumber { get; private set; }
		[Key(2)]
		public RoomType RoomType { get; private set; }
		[Key(3)]
		public TurnType NowTurnType { get; private set; }

		[Key(4)]
		public int _1stPlayerId { get; set; }
		[Key(5)]
		[ForeignKey("_1stPlayerId")]
		public PlayerEntity _1stPlayer { get; private set; }

		[Key(6)]
		public int _2ndPlayerId { get; set; }
		[Key(7)]
		[ForeignKey("_2ndPlayerId")]
		public PlayerEntity _2ndPlayer { get; private set; }

		[Key(8)]
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
			PanelAreaList = GetInitialPanelArea();
		}

		public override void SetId(int id)
		{
			RoomId = id;
		}

		[IgnoreMember]
		public bool IsSingle
		{
			get {
				return RoomType == RoomType.Single;
			}
		}

		[IgnoreMember]
		public bool IsMulti
		{
			get {
				return RoomType == RoomType.Multi;
			}
		}

		[IgnoreMember]
		public bool IsClear
		{
			get {
				var list = PanelAreaList.Where(x => x.TurnType == TurnType._1stPlayer).ToList();
				var isClear = isClearWithPanelAreaList(list);
				if (isClear) return true;

				list = PanelAreaList.Where(x => x.TurnType == TurnType._2ndPlayer).ToList();
				isClear = isClearWithPanelAreaList(list);
				if (isClear) return true;

				return false;
			}
		}


		[IgnoreMember]
		public bool IsEnd
		{
			get {
				return PanelAreaList.All(x => x.Selected);
			}
		}

		[IgnoreMember]
		public ResultType _1stPlayerResult
		{
			get {
				return GetResult(TurnType._1stPlayer);
			}
		}

		[IgnoreMember]
		public ResultType _2ndPlayerResult
		{
			get {
				return GetResult(TurnType._2ndPlayer);
			}
		}

		[IgnoreMember]
		public bool CanPlayAI
		{
			get {
				return RoomType == RoomType.Single && !IsClear && !IsEnd;
			}
		}


		[IgnoreMember]
		public bool Exsists2ndPlayer
		{
			get {
				return _2ndPlayer != null;
			}
		}

		public void Set2ndPlayer(PlayerEntity player)
		{
			_2ndPlayer = player;
		}

		public PanelAreaType SelectPanelArea(PlayerEntity player, PanelAreaType panelAreaType)
		{
			var turnType = player.Equals(_1stPlayer) ? TurnType._1stPlayer : TurnType._2ndPlayer;
			var panel = PanelAreaList.FirstOrDefault(x => x.PanelAreaType == panelAreaType);
			panel.SetTurnType(turnType);

			return panelAreaType;
		}

		public PanelAreaType SelectPanelAreaByAI()
		{
			var canSelectPanelAreaList = PanelAreaList.Where(x => !x.Selected).ToList();
			var random = new Random();
			var index = random.Next(canSelectPanelAreaList.Count);
			var panelAreaType = canSelectPanelAreaList[index].PanelAreaType;
			SelectPanelArea(_2ndPlayer, panelAreaType);

			return panelAreaType;
		}

		public TurnType NextTurn()
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

			return NowTurnType;
		}

		bool isClearWithPanelAreaList(List<PanelAreaEntity> list)
		{
			var selectedArea1 = list.Exists(x => x.PanelAreaType == PanelAreaType.Area1 && x.Selected);
			var selectedArea2 = list.Exists(x => x.PanelAreaType == PanelAreaType.Area2 && x.Selected);
			var selectedArea3 = list.Exists(x => x.PanelAreaType == PanelAreaType.Area3 && x.Selected);
			var selectedArea4 = list.Exists(x => x.PanelAreaType == PanelAreaType.Area4 && x.Selected);
			var selectedArea5 = list.Exists(x => x.PanelAreaType == PanelAreaType.Area5 && x.Selected);
			var selectedArea6 = list.Exists(x => x.PanelAreaType == PanelAreaType.Area6 && x.Selected);
			var selectedArea7 = list.Exists(x => x.PanelAreaType == PanelAreaType.Area7 && x.Selected);
			var selectedArea8 = list.Exists(x => x.PanelAreaType == PanelAreaType.Area8 && x.Selected);
			var selectedArea9 = list.Exists(x => x.PanelAreaType == PanelAreaType.Area9 && x.Selected);

			// Vertical
			var isClear = selectedArea1 && selectedArea2 && selectedArea3;
			if (isClear) return true;
			isClear = selectedArea4 && selectedArea5 && selectedArea6;
			if (isClear) return true;
			isClear = selectedArea7 && selectedArea8 && selectedArea9;
			if (isClear) return true;

			// Horizontal
			isClear = selectedArea1 && selectedArea4 && selectedArea7;
			if (isClear) return true;
			isClear = selectedArea2 && selectedArea5 && selectedArea8;
			if (isClear) return true;
			isClear = selectedArea3 && selectedArea6 && selectedArea9;
			if (isClear) return true;

			// Diagonal
			isClear = selectedArea1 && selectedArea5 && selectedArea9;
			if (isClear) return true;
			isClear = selectedArea3 && selectedArea5 && selectedArea7;
			if (isClear) return true;

			return false;
		}

		ResultType GetResult(TurnType turnType)
		{
			var list = PanelAreaList.Where(x => x.TurnType == TurnType._1stPlayer).ToList();
			var isClear = isClearWithPanelAreaList(list);
			if(isClear){
				if(turnType == TurnType._1stPlayer){
					return ResultType.Win;
				} else {
					return ResultType.Lose;
				}
			}

			list = PanelAreaList.Where(x => x.TurnType == TurnType._2ndPlayer).ToList();
			isClear = isClearWithPanelAreaList(list);
			if (isClear) {
				if (turnType == TurnType._1stPlayer) {
					return ResultType.Lose;
				} else {
					return ResultType.Win;
				}
			}

			if(IsEnd){
				return ResultType.Draw;
			}

			return ResultType.None;
		}

		List<PanelAreaEntity> GetInitialPanelArea()
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

		public override bool Equals(object obj)
		{
			var entity = obj as RoomEntity;
			return entity != null &&
				   RoomId == entity.RoomId;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(RoomId);
		}
	}
}
