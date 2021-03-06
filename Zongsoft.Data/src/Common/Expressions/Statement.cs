﻿/*
 *   _____                                ______
 *  /_   /  ____  ____  ____  _________  / __/ /_
 *    / /  / __ \/ __ \/ __ \/ ___/ __ \/ /_/ __/
 *   / /__/ /_/ / / / / /_/ /\_ \/ /_/ / __/ /_
 *  /____/\____/_/ /_/\__  /____/\____/_/  \__/
 *                   /____/
 *
 * Authors:
 *   钟峰(Popeye Zhong) <zongsoft@gmail.com>
 *
 * Copyright (C) 2010-2020 Zongsoft Studio <http://www.zongsoft.com>
 *
 * This file is part of Zongsoft.Data library.
 *
 * The Zongsoft.Data is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3.0 of the License,
 * or (at your option) any later version.
 *
 * The Zongsoft.Data is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with the Zongsoft.Data library. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;

using Zongsoft.Collections;
using Zongsoft.Data.Metadata;

namespace Zongsoft.Data.Common.Expressions
{
	/// <summary>
	/// 表示带条件子句的语句基类。
	/// </summary>
	public class Statement : StatementBase, IStatement
	{
		#region 私有变量
		private int _aliasIndex;
		#endregion

		#region 构造函数
		protected Statement()
		{
			this.From = new SourceCollection();
		}

		protected Statement(ISource source)
		{
			this.Table = source as TableIdentifier;
			this.From = new SourceCollection();

			if(source != null)
				this.From.Add(source);
		}

		protected Statement(IDataEntity entity, string alias = null) : base(entity, alias)
		{
			this.From = new SourceCollection();
			this.From.Add(this.Table);
		}
		#endregion

		#region 公共属性
		public bool HasFrom
		{
			get => this.From != null && this.From.Count > 0;
		}

		/// <summary>
		/// 获取一个数据源的集合，可以在 Where 子句中引用的字段源。
		/// </summary>
		public INamedCollection<ISource> From
		{
			get;
		}

		/// <summary>
		/// 获取或设置条件子句。
		/// </summary>
		public IExpression Where
		{
			get;
			set;
		}
		#endregion

		#region 公共方法
		/// <summary>
		/// 获取或创建指定源与实体的继承关联子句。
		/// </summary>
		/// <param name="source">指定要创建关联子句的源。</param>
		/// <param name="target">指定要创建关联子句的目标实体。</param>
		/// <param name="fullPath">指定的 <paramref name="target"/> 参数对应的目标实体关联的成员的完整路径。</param>
		/// <returns>返回已存在或新创建的继承表关联子句。</returns>
		public JoinClause Join(ISource source, IDataEntity target, string fullPath = null)
		{
			var clause = JoinClause.Create(source,
			                               target,
			                               fullPath,
			                               name => this.From.TryGet(name, out var join) ? (JoinClause)join : null,
			                               entity => this.CreateTableReference(entity));

			if(!this.From.Contains(clause))
				this.From.Add(clause);

			return clause;
		}

		/// <summary>
		/// 获取或创建指定导航属性的关联子句。
		/// </summary>
		/// <param name="source">指定要创建关联子句的源。</param>
		/// <param name="complex">指定要创建关联子句对应的导航属性。</param>
		/// <param name="fullPath">指定的 <paramref name="complex"/> 参数对应的成员完整路径。</param>
		/// <returns>返回已存在或新创建的导航关联子句。</returns>
		public JoinClause Join(ISource source, IDataEntityComplexProperty complex, string fullPath = null)
		{
			var joins = JoinClause.Create(source,
			                              complex,
			                              fullPath,
			                              name => this.From.TryGet(name, out var join) ? (JoinClause)join : null,
			                              entity => this.CreateTableReference(entity));

			JoinClause last = null;

			foreach(var join in joins)
			{
				if(!this.From.Contains(join))
					this.From.Add(join);

				last = join;
			}

			//返回最后一个Join子句
			return last;
		}

		/// <summary>
		/// 获取或创建导航属性的关联子句。
		/// </summary>
		/// <param name="source">指定要创建关联子句的源。</param>
		/// <param name="schema">指定要创建关联子句对应的数据模式成员。</param>
		/// <returns>返回已存在或新创建的导航关联子句，如果 <paramref name="schema"/> 参数指定的数据模式成员对应的不是导航属性则返回空(null)。</returns>
		public JoinClause Join(ISource source, SchemaMember schema)
		{
			if(schema.Token.Property.IsSimplex)
				return null;

			return this.Join(source, (IDataEntityComplexProperty)schema.Token.Property, schema.FullPath);
		}
		#endregion

		#region 保护方法
		internal protected TableIdentifier CreateTableReference(IDataEntity entity)
		{
			return new TableIdentifier(entity, "T" + (++_aliasIndex).ToString());
		}
		#endregion
	}
}
