namespace Isp.Laboratorios.Infrastructure
{
    public class AlertMessage
    {
        public string AlertType { get; set; }
        public string Message { get; set; }
        public bool Dismissable { get; set; }
        public string Glyphicon { get; set; }
    }
}