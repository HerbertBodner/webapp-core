namespace WaCore.Contracts.Entities.Core
{
    public interface IEmailTemplate : ITemplate
    {
        string Subject { get; set; }
        string From { get; set; }
        string To { get; set; }
        string Cc { get; set; }
        string Bcc { get; set; }
    }
}