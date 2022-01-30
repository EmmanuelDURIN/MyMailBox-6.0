namespace MyMailBox.Models
{
  public class MailBox
  {
    public string Reference { get; set; } = "";
    public string Name { get; set; } = "";
    public string Color { get; set; } = "";
    public double Height { get; set; }
    public double Width { get; set; }
    public double Depth { get; set; }
    public string? ImagePath { get; set; }
  }
}
