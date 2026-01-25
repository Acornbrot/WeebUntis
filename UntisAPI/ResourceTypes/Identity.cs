namespace UntisAPI.ResourceTypes
{
    public class Identity
    {
        public required Student Student { get; set; }
        public required List<Class> Classes { get; set; }
    }
}
