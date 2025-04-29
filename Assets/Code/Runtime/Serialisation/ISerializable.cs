namespace Code.Runtime.Serialisation
{
    public interface ISerializable<T> where T : AbstractMemento
    {
        public abstract T Serialize();
        public abstract void Deserialize(T memento);
    }
}