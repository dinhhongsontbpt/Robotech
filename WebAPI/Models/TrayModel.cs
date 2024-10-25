namespace WebAPI.Models
{
    public class TrayModel
    {
        public int ModelNo { get; set; }
        public string? ModelName { get; set; }
        public int[] ScrewType { get; set; } = new int[84]; //Loại vít 0~28 tương ứng với các hộp cấp vít, bằng 0 là không có vít
        public int[] IsGlue {  get; set; } = new int[84]; //Có bôi keo hay không 0: không bôi, 1: có bôi
    }
}
