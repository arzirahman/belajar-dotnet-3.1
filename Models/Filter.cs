namespace Coba_Net.Models
{
    public class Filter<T>
    {
        public string Label { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public T Value{ get; set; }
    }
}