public class ParkingLotDTO
{
    public int LotId { get; set; }
    public string LotSector { get; set; }
    public int? UserId { get; set; } // Sử dụng kiểu nullable để xử lý null
    public string Name { get; set; }
    public string LicensePlate { get; set; }
    public string Status { get; set; }
    public int? Amount { get; set; }
}
