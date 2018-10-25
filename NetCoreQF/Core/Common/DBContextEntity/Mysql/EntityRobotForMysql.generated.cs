using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AntData.ORM;
using AntData.ORM.Data;
using AntData.ORM.Linq;
using AntData.ORM.Mapping;

namespace DbModels.Mysql
{
	/// <summary>
	/// Database       : testorm
	/// Data Source    : localhost
	/// Server Version : 5.6.26-log
	/// </summary>
	public partial class TestormEntitys : IEntity
	{
		/// <summary>
		/// ��Ա
		/// </summary>
		public IQueryable<Person>      People      { get { return this.Get<Person>(); } }
		/// <summary>
		/// ѧУ
		/// </summary>
		public IQueryable<School>      Schools     { get { return this.Get<School>(); } }
		public IQueryable<SchoolName>  SchoolName  { get { return this.Get<SchoolName>(); } }
		public IQueryable<school_name> school_name { get { return this.Get<school_name>(); } }

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

		public TestormEntitys(DataConnection con)
		{
			this.con = con;
		}
	}

	/// <summary>
	/// ��Ա
	/// </summary>
	[Table(Comment="��Ա", Name="person")]
	public partial class Person : BaseEntity
	{
		#region Column

		/// <summary>
		/// ����
		/// </summary>
		[Column("Id",                  DataType=AntData.ORM.DataType.Int64,    Comment="����"), PrimaryKey, Identity]
		public long Id { get; set; } // bigint(20)

		/// <summary>
		/// ������ʱ��
		/// </summary>
		[Column("DataChange_LastTime", DataType=AntData.ORM.DataType.DateTime, Comment="������ʱ��"), NotNull]
		public DateTime DataChangeLastTime // datetime
		{
			get { return _DataChangeLastTime; }
			set { _DataChangeLastTime = value; }
		}

		/// <summary>
		/// ����
		/// </summary>
		[Column("Name",                DataType=AntData.ORM.DataType.VarChar,  Length=50, Comment="����"), NotNull]
		public string Name { get; set; } // varchar(50)

		/// <summary>
		/// ���
		/// </summary>
		[Column("Age",                 DataType=AntData.ORM.DataType.Int32,    Comment="���"),    Nullable]
		public int? Age { get; set; } // int(11)

		/// <summary>
		/// ѧУ����
		/// </summary>
		[Column("SchoolId",            DataType=AntData.ORM.DataType.Int64,    Comment="ѧУ����"),    Nullable]
		public long? SchoolId { get; set; } // bigint(20)

		#endregion

		#region Field

		private DateTime _DataChangeLastTime = System.Data.SqlTypes.SqlDateTime.MinValue.Value;

		#endregion

		#region Associations

		/// <summary>
		/// person_SchoolId_school_Id
		/// </summary>
		[Association(ThisKey="SchoolId", OtherKey="Id", CanBeNull=true, IsBackReference=true)]
		public School School { get; set; }

		#endregion
	}

	/// <summary>
	/// ѧУ
	/// </summary>
	[Table(Comment="ѧУ", Name="school")]
	public partial class School : BaseEntity
	{
		#region Column

		/// <summary>
		/// ����
		/// </summary>
		[Column("Id",                  DataType=AntData.ORM.DataType.Int64,    Comment="����"), PrimaryKey, Identity]
		public long Id { get; set; } // bigint(20)

		/// <summary>
		/// ѧУ����
		/// </summary>
		[Column("Name",                DataType=AntData.ORM.DataType.VarChar,  Length=50, Comment="ѧУ����"),    Nullable]
		public string Name { get; set; } // varchar(50)

		/// <summary>
		/// ѧУ��ַ
		/// </summary>
		[Column("Address",             DataType=AntData.ORM.DataType.VarChar,  Length=100, Comment="ѧУ��ַ"),    Nullable]
		public string Address { get; set; } // varchar(100)

		/// <summary>
		/// ������ʱ��
		/// </summary>
		[Column("DataChange_LastTime", DataType=AntData.ORM.DataType.DateTime, Comment="������ʱ��"), NotNull]
		public DateTime DataChangeLastTime // datetime
		{
			get { return _DataChangeLastTime; }
			set { _DataChangeLastTime = value; }
		}

		#endregion

		#region Field

		private DateTime _DataChangeLastTime = System.Data.SqlTypes.SqlDateTime.MinValue.Value;

		#endregion

		#region Associations

		/// <summary>
		/// school_Id_person_SchoolId
		/// </summary>
		[Association(ThisKey="Id", OtherKey="SchoolId", CanBeNull=true, IsBackReference=true)]
		public IEnumerable<Person> PersonList { get; set; }

		#endregion
	}

	[Table("school__name")]
	public partial class SchoolName : BaseEntity
	{
		#region Column

		/// <summary>
		/// ����
		/// </summary>
		[Column("Id", DataType=AntData.ORM.DataType.Int64, Comment="����"), PrimaryKey, Identity]
		public long Id { get; set; } // bigint(20)

		#endregion
	}

	[Table("school_name")]
	public partial class school_name : BaseEntity
	{
		#region Column

		/// <summary>
		/// ����
		/// </summary>
		[Column("Id",      DataType=AntData.ORM.DataType.Int64,   Comment="����"), PrimaryKey, NotNull]
		public long Id { get; set; } // bigint(20)

		/// <summary>
		/// ����
		/// </summary>
		[Column("Name",    DataType=AntData.ORM.DataType.Boolean, Comment="����"), NotNull]
		public bool Name { get; set; } // bit(1)

		/// <summary>
		/// ������
		/// </summary>
		[Column("OrderID", DataType=AntData.ORM.DataType.Int64,   Comment="������"), NotNull]
		public long OrderID { get; set; } // bigint(20)

		#endregion
	}

	public static partial class TableExtensions
	{
		public static Person FindByBk(this IQueryable<Person> table, long Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static async Task<Person> FindByBkAsync(this IQueryable<Person> table, long Id)
		{
			return await table.FirstOrDefaultAsync(t =>
				t.Id == Id);
		}

		public static School FindByBk(this IQueryable<School> table, long Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static async Task<School> FindByBkAsync(this IQueryable<School> table, long Id)
		{
			return await table.FirstOrDefaultAsync(t =>
				t.Id == Id);
		}

		public static SchoolName FindByBk(this IQueryable<SchoolName> table, long Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static async Task<SchoolName> FindByBkAsync(this IQueryable<SchoolName> table, long Id)
		{
			return await table.FirstOrDefaultAsync(t =>
				t.Id == Id);
		}

		public static school_name FindByBk(this IQueryable<school_name> table, long Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static async Task<school_name> FindByBkAsync(this IQueryable<school_name> table, long Id)
		{
			return await table.FirstOrDefaultAsync(t =>
				t.Id == Id);
		}
	}
}
