namespace LandMark.API.Entities
{
    /// <summary>
    /// This entity class is used to construct position parameter and pass it filter function
    /// </summary>
    public class Position
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
    }
}
