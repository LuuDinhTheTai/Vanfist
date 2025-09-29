namespace Vanfist.DTOs.Responses
{
    public class ModelResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Wheelbase { get; set; }
        public float NEDC { get; set; }
        public float MaximumPower { get; set; }
        public float MaximumTorque { get; set; }
        public float RimSize { get; set; }
        public string CategoryName { get; set; }
        public string Color { get; set; }
        public List<string> Attachments { get; set; }
    }
}
