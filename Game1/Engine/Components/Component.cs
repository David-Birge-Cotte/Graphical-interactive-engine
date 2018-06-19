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

        public virtual void Update(float dt)
        {

        }

        public virtual void PostUpdate(float dt)
        {

        }

		public virtual void OnDestroy()
		{
			
		}
    }
}
