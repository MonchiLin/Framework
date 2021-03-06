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
 * This file is part of Zongsoft.Commands library.
 *
 * The Zongsoft.Commands is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3.0 of the License,
 * or (at your option) any later version.
 *
 * The Zongsoft.Commands is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with the Zongsoft.Commands library. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;

using Zongsoft.Services;

namespace Zongsoft.Messaging.Commands
{
	public class ListenerStopCommand : CommandBase<CommandContext>
	{
		#region 构造函数
		public ListenerStopCommand() : base("Stop")
		{
		}

		public ListenerStopCommand(string name) : base(name)
		{
		}
		#endregion

		#region 重写方法
		protected override object OnExecute(CommandContext context)
		{
			var listener = ListenerCommand.GetListener(context.CommandNode);

			if(listener == null)
				throw new CommandException(string.Format(Properties.Resources.Text_CannotObtainCommandTarget, "Server"));

			if(listener.IsListening)
				listener.Stop();

			if(listener.IsListening)
				context.Output.WriteLine(CommandOutletColor.Red, Properties.Resources.Text_CommandExecuteFailed);
			else
				context.Output.WriteLine(CommandOutletColor.Green, Properties.Resources.Text_CommandExecuteSucceed);

			return !listener.IsListening;
		}
		#endregion
	}
}
