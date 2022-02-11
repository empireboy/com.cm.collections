using CM.ObjectCreation;
using System.Collections.Generic;

namespace CM.Collections
{
	public class ObjectPool<T> : IObjectPool<T>
	{
		public int MaxObjects { get; set; }
		public bool IsEmpty => reusables.Count <= 0;
		public int Count => reusables.Count;
		public T DefaultObject { get; set; }
		public IObjectCreator<T> ObjectCreator { get; set; }

		protected Stack<T> reusables = new Stack<T>();

		public ObjectPool(int maxObjects)
		{
			MaxObjects = maxObjects;
		}

		public ObjectPool(IObjectCreator<T> objectCreator, T defaultObject, int maxObjects)
		{
			DefaultObject = defaultObject;
			ObjectCreator = objectCreator;
			MaxObjects = maxObjects;
		}

		public void AddReusable(T reusable)
		{
			if (reusables.Count >= MaxObjects)
			{
				ObjectCreator.DestroyObject(reusable);
				return;
			}

			reusables.Push(reusable);
		}

		public T GetReusable()
		{
			T reusable;

			if (!IsEmpty)
				reusable = reusables.Pop();
			else
				reusable = ObjectCreator.CloneObject(DefaultObject);

			return reusable;
		}
	}
}