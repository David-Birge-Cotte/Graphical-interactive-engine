using System;
using System.Collections.Generic;

namespace Game1.Engine
{
    public class Component
    {
		public Entity Entity;

		public Component()
		{

		}

		public virtual void Initialize()
		{
			
		}

        public virtual void Update()
        {

        }

		public virtual void OnDestroy()
		{
			
		}
    }
}
