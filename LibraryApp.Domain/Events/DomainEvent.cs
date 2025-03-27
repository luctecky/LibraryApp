using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Domain.Events
{
	public class DomainEvent
	{
		public DateTime Timestamp { get; }

		protected DomainEvent()
		{
			Timestamp = DateTime.Now;
		}
	}
}