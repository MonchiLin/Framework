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

namespace Zongsoft.Security.Membership
{
	/// <summary>
	/// 表示身份验证器的接口。
	/// </summary>
	public interface IAuthenticator
	{
		#region 事件定义
		/// <summary>
		/// 表示验证完成的事件。
		/// </summary>
		event EventHandler<AuthenticatedEventArgs> Authenticated;

		/// <summary>
		/// 表示验证开始的事件。
		/// </summary>
		event EventHandler<AuthenticatingEventArgs> Authenticating;
		#endregion

		#region 属性定义
		/// <summary>
		/// 获取验证器的方案名。
		/// </summary>
		string Scheme { get; }
		#endregion

		#region 方法定义
		/// <summary>
		/// 验证指定名称的用户是否有效并且和指定的密码是否完全匹配。
		/// </summary>
		/// <param name="identity">要验证的用户标识，可以是“用户名”、“手机号码”或者“邮箱地址”。</param>
		/// <param name="password">指定用户的密码。</param>
		/// <param name="namespace">要验证的用户标识所属的命名空间。</param>
		/// <param name="scenario">指定的验证应用场景。</param>
		/// <param name="parameters">指定的扩展参数集。</param>
		/// <returns>如果验证的结果对象。</returns>
		AuthenticationResult Authenticate(string identity, string password, string @namespace, string scenario, IDictionary<string, object> parameters);

		/// <summary>
		/// 验证指定名称的用户是否有效并且和指定的验证码是否有效。
		/// </summary>
		/// <param name="identity">要验证的用户标识，仅限用户的“Phone”或“Email”。</param>
		/// <param name="secret">指定用户的验证码，首先需要通过<see cref="Secret(string, string)"/>方法获得验证码。</param>
		/// <param name="namespace">要验证的用户标识所属的命名空间。</param>
		/// <param name="scenario">指定的验证应用场景。</param>
		/// <param name="parameters">指定的扩展参数集。</param>
		/// <returns>如果验证的结果对象。</returns>
		AuthenticationResult AuthenticateSecret(string identity, string secret, string @namespace, string scenario, IDictionary<string, object> parameters);

		/// <summary>
		/// 验证指定编号的用户身份。
		/// </summary>
		/// <param name="userId">要验证的用户编号。</param>
		/// <param name="password">验证的用户密码。</param>
		/// <returns>返回验证成功或失败的缘由。</returns>
		AuthenticationReason Verify(uint userId, string password);

		/// <summary>
		/// 生成一个验证码，并将其发送到指定用户标识所对应的手机或电子邮箱中。
		/// </summary>
		/// <param name="identity">要获取的用户标识，仅限用户的“Phone”或“Email”。</param>
		/// <param name="namespace">指定用户标识所属的命名空间。</param>
		void Secret(string identity, string @namespace);
		#endregion
	}
}
