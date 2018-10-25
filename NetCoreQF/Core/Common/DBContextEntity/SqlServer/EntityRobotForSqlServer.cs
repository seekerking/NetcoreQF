using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

using AntData.ORM;
using AntData.ORM.Data;
using AntData.ORM.Linq;
using AntData.ORM.Mapping;

namespace DbModels.SqlServer
{
	/// <summary>
	/// Database       : WxUtilDB
	/// Data Source    : 192.168.1.110,1478
	/// Server Version : 10.00.1600
	/// </summary>
	public partial class WxUtilDBEntitys : IEntity
	{
		public IQueryable<ConfigDic>              ConfigDics              { get { return this.Get<ConfigDic>(); } }
		public IQueryable<Holiday>                Holidays                { get { return this.Get<Holiday>(); } }
		public IQueryable<LConfig>                LConfigs                { get { return this.Get<LConfig>(); } }
		public IQueryable<LotteryDraw>            LotteryDraws            { get { return this.Get<LotteryDraw>(); } }
		public IQueryable<UserTB>                 UserTBs                 { get { return this.Get<UserTB>(); } }
		public IQueryable<WxUserMessageSendLogTB> WxUserMessageSendLogTBs { get { return this.Get<WxUserMessageSendLogTB>(); } }
		public IQueryable<WxUserMessageTB>        WxUserMessageTBs        { get { return this.Get<WxUserMessageTB>(); } }

		private readonly DataConnection con;

		public DataConnection DbContext
		{
			get { return this.con; }
		}

		public IQueryable<T> Get<T>()
			 where T : class
		{
			return this.con.GetTable<T>();
		}

		public WxUtilDBEntitys(DataConnection con)
		{
			this.con = con;
		}
	}

	[Table(Schema="dbo", Name="ConfigDic")]
	public partial class ConfigDic : BaseEntity
	{
		#region Column

		[Column("Id",          DataType=AntData.ORM.DataType.Int32)  , PrimaryKey, Identity]
		public int Id { get; set; } // int

		[Column("Name",        DataType=AntData.ORM.DataType.VarChar, Length=50), NotNull]
		public string Name { get; set; } // varchar(50)

		[Column("Description", DataType=AntData.ORM.DataType.VarChar, Length=255),    Nullable]
		public string Description { get; set; } // varchar(255)

		[Column("Type",        DataType=AntData.ORM.DataType.Int32)  , NotNull]
		public int Type { get; set; } // int

		#endregion
	}

	[Table(Schema="dbo", Name="Holidays")]
	public partial class Holiday : BaseEntity
	{
		#region Column

		[Column("Id",          DataType=AntData.ORM.DataType.Int32)  , PrimaryKey, Identity]
		public int Id { get; set; } // int

		[Column("Name",        DataType=AntData.ORM.DataType.VarChar, Length=125), NotNull]
		public string Name { get; set; } // varchar(125)

		[Column("DayKey",      DataType=AntData.ORM.DataType.VarChar, Length=10), NotNull]
		public string DayKey { get; set; } // varchar(10)

		[Column("Type",        DataType=AntData.ORM.DataType.Int32)  , NotNull]
		public int Type { get; set; } // int

		[Column("Description", DataType=AntData.ORM.DataType.VarChar, Length=500), NotNull]
		public string Description { get; set; } // varchar(500)

		#endregion
	}

	[Table(Schema="dbo", Name="LConfig")]
	public partial class LConfig : BaseEntity
	{
		#region Column

		[Column("Id",    DataType=AntData.ORM.DataType.Int32), PrimaryKey, Identity]
		public int Id { get; set; } // int

		[Column("Type",  DataType=AntData.ORM.DataType.Int32), NotNull]
		public int Type { get; set; } // int

		[Column("Value", DataType=AntData.ORM.DataType.Int32), NotNull]
		public int Value { get; set; } // int

		#endregion
	}

	[Table(Schema="dbo", Name="LotteryDraw")]
	public partial class LotteryDraw : BaseEntity
	{
		#region Column

		[Column("Id",          DataType=AntData.ORM.DataType.Int32)    , PrimaryKey, Identity]
		public int Id { get; set; } // int

		[Column("ContactInfo", DataType=AntData.ORM.DataType.VarChar,   Length=500), NotNull]
		public string ContactInfo { get; set; } // varchar(500)

		[Column("Token",       DataType=AntData.ORM.DataType.VarChar,   Length=500), NotNull]
		public string Token { get; set; } // varchar(500)

		/// <summary>
		/// 1:一等奖 2：二等奖，3：三等奖，4：四等奖
		/// </summary>
		[Column("RewardType",  DataType=AntData.ORM.DataType.Int32,     Comment="1:一等奖 2：二等奖，3：三等奖，4：四等奖"), NotNull]
		public int RewardType { get; set; } // int

		[Column("CreateTime",  DataType=AntData.ORM.DataType.Int64)    ,    Nullable]
		public long? CreateTime { get; set; } // bigint

		[Column("ExpireTime",  DataType=AntData.ORM.DataType.Int64)    ,    Nullable]
		public long? ExpireTime { get; set; } // bigint

		[Column("TimeStamp",   DataType=AntData.ORM.DataType.Timestamp, SkipOnInsert=true, SkipOnUpdate=true), NotNull]
		public byte[] TimeStamp { get; set; } // timestamp

		/// <summary>
		/// 0:有效，1:已登记，2：超时作废
		/// </summary>
		[Column("Status",      DataType=AntData.ORM.DataType.Int32,     Comment="0:有效，1:已登记，2：超时作废"), NotNull]
		public int Status { get; set; } // int

		[Column("Ip",          DataType=AntData.ORM.DataType.VarChar,   Length=50), NotNull]
		public string Ip { get; set; } // varchar(50)

		#endregion
	}

	[Table(Schema="dbo", Name="UserTB")]
	public partial class UserTB : BaseEntity
	{
		#region Column

		[Column("Id",         DataType=AntData.ORM.DataType.Int32)   , Identity]
		public int Id { get; set; } // int

		[Column("UserId",     DataType=AntData.ORM.DataType.VarChar,  Length=50),    Nullable]
		public string UserId { get; set; } // varchar(50)

		[Column("Pwd",        DataType=AntData.ORM.DataType.VarChar,  Length=200),    Nullable]
		public string Pwd { get; set; } // varchar(200)

		[Column("Name",       DataType=AntData.ORM.DataType.NVarChar, Length=50),    Nullable]
		public string Name { get; set; } // nvarchar(50)

		[Column("CreateDate", DataType=AntData.ORM.DataType.DateTime), NotNull]
		public DateTime CreateDate // datetime
		{
			get { return _CreateDate; }
			set { _CreateDate = value; }
		}

		[Column("LastUpdate", DataType=AntData.ORM.DataType.DateTime), NotNull]
		public DateTime LastUpdate // datetime
		{
			get { return _LastUpdate; }
			set { _LastUpdate = value; }
		}

		[Column("Status",     DataType=AntData.ORM.DataType.Int32)   , NotNull]
		public int Status { get; set; } // int

		#endregion

		#region Field

		private DateTime _CreateDate = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
		private DateTime _LastUpdate = System.Data.SqlTypes.SqlDateTime.MinValue.Value;

		#endregion
	}

	[Table(Schema="dbo", Name="Wx_UserMessageSendLogTB")]
	public partial class WxUserMessageSendLogTB : BaseEntity
	{
		#region Column

		[Column("Id",       DataType=AntData.ORM.DataType.Int32)   , PrimaryKey, Identity]
		public int Id { get; set; } // int

		[Column("WxOpenId", DataType=AntData.ORM.DataType.VarChar,  Length=128), NotNull]
		public string WxOpenId { get; set; } // varchar(128)

		[Column("Status",   DataType=AntData.ORM.DataType.Int32)   , NotNull]
		public int Status { get; set; } // int

		[Column("SendDate", DataType=AntData.ORM.DataType.DateTime), NotNull]
		public DateTime SendDate // datetime
		{
			get { return _SendDate; }
			set { _SendDate = value; }
		}

		#endregion

		#region Field

		private DateTime _SendDate = System.Data.SqlTypes.SqlDateTime.MinValue.Value;

		#endregion
	}

	[Table(Schema="dbo", Name="Wx_UserMessageTB")]
	public partial class WxUserMessageTB : BaseEntity
	{
		#region Column

		[Column("Id",         DataType=AntData.ORM.DataType.Int32)   , PrimaryKey, Identity]
		public int Id { get; set; } // int

		[Column("WxOpenId",   DataType=AntData.ORM.DataType.VarChar,  Length=128), NotNull]
		public string WxOpenId { get; set; } // varchar(128)

		[Column("WxToken",    DataType=AntData.ORM.DataType.VarChar,  Length=128), NotNull]
		public string WxToken { get; set; } // varchar(128)

		[Column("Status",     DataType=AntData.ORM.DataType.Int32)   , NotNull]
		public int Status { get; set; } // int

		[Column("CreateTime", DataType=AntData.ORM.DataType.DateTime), NotNull]
		public DateTime CreateTime // datetime
		{
			get { return _CreateTime; }
			set { _CreateTime = value; }
		}

		[Column("Message",    DataType=AntData.ORM.DataType.VarChar,  Length=200),    Nullable]
		public string Message { get; set; } // varchar(200)

		[Column("ExpireTime", DataType=AntData.ORM.DataType.DateTime), NotNull]
		public DateTime ExpireTime // datetime
		{
			get { return _ExpireTime; }
			set { _ExpireTime = value; }
		}

		[Column("SendTime",   DataType=AntData.ORM.DataType.DateTime),    Nullable]
		public DateTime? SendTime { get; set; } // datetime

		#endregion

		#region Field

		private DateTime _CreateTime = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
		private DateTime _ExpireTime = System.Data.SqlTypes.SqlDateTime.MinValue.Value;

		#endregion
	}

	public static partial class TableExtensions
	{
		public static ConfigDic FindByBk(this IQueryable<ConfigDic> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static async Task<ConfigDic> FindByBkAsync(this IQueryable<ConfigDic> table, int Id)
		{
			return await table.FirstOrDefaultAsync(t =>
				t.Id == Id);
		}

		public static Holiday FindByBk(this IQueryable<Holiday> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static async Task<Holiday> FindByBkAsync(this IQueryable<Holiday> table, int Id)
		{
			return await table.FirstOrDefaultAsync(t =>
				t.Id == Id);
		}

		public static LConfig FindByBk(this IQueryable<LConfig> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static async Task<LConfig> FindByBkAsync(this IQueryable<LConfig> table, int Id)
		{
			return await table.FirstOrDefaultAsync(t =>
				t.Id == Id);
		}

		public static LotteryDraw FindByBk(this IQueryable<LotteryDraw> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static async Task<LotteryDraw> FindByBkAsync(this IQueryable<LotteryDraw> table, int Id)
		{
			return await table.FirstOrDefaultAsync(t =>
				t.Id == Id);
		}

		public static WxUserMessageSendLogTB FindByBk(this IQueryable<WxUserMessageSendLogTB> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static async Task<WxUserMessageSendLogTB> FindByBkAsync(this IQueryable<WxUserMessageSendLogTB> table, int Id)
		{
			return await table.FirstOrDefaultAsync(t =>
				t.Id == Id);
		}

		public static WxUserMessageTB FindByBk(this IQueryable<WxUserMessageTB> table, int Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static async Task<WxUserMessageTB> FindByBkAsync(this IQueryable<WxUserMessageTB> table, int Id)
		{
			return await table.FirstOrDefaultAsync(t =>
				t.Id == Id);
		}
	}
}
