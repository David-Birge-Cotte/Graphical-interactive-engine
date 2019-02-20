using System;

namespace CoreEngine.ECS
{
    public class Component
    {
        protected Entity Entity;
		private bool isInitialized = false;

		protected Transform Transform 
		{ 
			get {
				return Entity.Transform;
			}
		}

        public Component()
        {

        }

        public virtual void Initialize(Entity entity)
		{
			Entity = entity;
			isInitialized = true;
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

		// Event when entity/parent entity is activated ?
		public void OnEnable(Entity entity)
		{
			if (!isInitialized)
				Initialize(entity);
		}
    }
}