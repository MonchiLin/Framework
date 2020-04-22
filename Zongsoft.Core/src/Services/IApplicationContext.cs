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
 * This file is part of Zongsoft.Core library.
 *
 * The Zongsoft.Core is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3.0 of the License,
 * or (at your option) any later version.
 *
 * The Zongsoft.Core is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with the Zongsoft.Core library. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;

using Microsoft.Extensions.Configuration;

using Zongsoft.Collections;

namespace Zongsoft.Services
{
	/// <summary>
	/// 表示应用程序上下文的接口。
	/// </summary>
	public interface IApplicationContext : IApplicationModule
	{
		#region 事件定义
		event EventHandler Exiting;
		event EventHandler Started;
		#endregion

		#region 属性定义
		/// <summary>
		/// 获取当前应用程序的根目录。
		/// </summary>
		string ApplicationDirectory
		{
			get;
		}

		/// <summary>
		/// 获取当前应用程序的应用配置。
		/// </summary>
		IConfiguration Configuration
		{
			get;
		}

		/// <summary>
		/// 获取当前应用程序的环境信息。
		/// </summary>
		IApplicationEnvironment Environment
		{
			get;
		}

		/// <summary>
		/// 获取当前应用程序的安全主体。
		/// </summary>
		System.Security.Claims.ClaimsPrincipal Principal
		{
			get;
		}

		/// <summary>
		/// 获取当前应用程序的模块集。
		/// </summary>
		INamedCollection<IApplicationModule> Modules
		{
			get;
		}

		/// <summary>
		/// 获取当前应用程序的初始化器集。
		/// </summary>
		ICollection<IApplicationInitializer> Initializers
		{
			get;
		}
		#endregion

		#region 方法定义
		/// <summary>
		/// 确认指定的当前应用程序的相对目录是否存在，如果不存在则依次创建它们，并返回其对应的完整路径。
		/// </summary>
		/// <param name="relativePath">相对于应用程序根目录的相对路径，可使用'/'或'\'字符作为相对路径的分隔符。</param>
		/// <returns>如果<paramref name="relativePath"/>参数为空或者全空白字符则返回应用程序根目录(即<see cref="ApplicationDirectory"/>属性值。)，否则返回其相对路径的完整路径。</returns>
		string EnsureDirectory(string relativePath);
		#endregion
	}
}
