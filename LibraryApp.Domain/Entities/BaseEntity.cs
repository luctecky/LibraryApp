using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Domain.Entities
{
	public abstract class BaseEntity
	{
		public Guid Id { get; protected set; }
		public DateTime CreatedAt { get; private set; }
		public DateTime? UpdatedAt { get; private set; }

		protected BaseEntity()
		{
			Id = Guid.NewGuid();
			CreatedAt = DateTime.UtcNow;
		}

		public void UpdatedNow()
		{
			UpdatedAt = DateTime.UtcNow;
		}
	}
}