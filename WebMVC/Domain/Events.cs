﻿using System;

namespace WebMVC
{
	public class Event : EntityBase
	{
		public virtual string Description { get; set; }

		public virtual UserInfo GeneratedBy { get; set; }
		public virtual string Title { get; set; }
		public virtual DateTime When { get; set; }
		public virtual string Where { get; set; }
	}
}