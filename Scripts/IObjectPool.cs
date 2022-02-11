namespace CM.Collections
{
	public interface IObjectPool<T>
	{
		int MaxObjects { get; set; }
		bool IsEmpty { get; }
		int Count { get; }

		void AddReusable(T reusable);
		T GetReusable();
	}
}