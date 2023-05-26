namespace Northwind_App.Interfaces.Common
{
    public interface ITEntity<ITemid>
    {
        ITemId RowID { get; set; }
    }
}
