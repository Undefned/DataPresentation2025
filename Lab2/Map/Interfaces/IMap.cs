namespace Lab2.Map.Interfaces;

public interface IMap<TKey, TValue>
{
    void MakeNull();
    void Assign(TKey key, TValue value);
    bool Compute(TKey key, out TValue value);
    void Print();
}