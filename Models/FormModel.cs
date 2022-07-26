namespace DrawingsWebApp.Models
{
    public class FormModel
    {
        public UserModes currentUserMode { get; set; }
    }

    public enum UserModes
    {
        engineer, writer
    }
    
}
