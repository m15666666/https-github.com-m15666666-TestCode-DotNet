using System;

namespace WebMVC
{
	public class UserInfo : EntityBase
	{
		public virtual int Email
		{
			get { throw new NotImplementedException(); }
			set { }
		}

		public virtual int Username
		{
			get { throw new NotImplementedException(); }
			set { }
		}
	}
}