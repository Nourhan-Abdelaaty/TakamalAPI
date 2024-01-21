
namespace Domain.Models;
public class BaseModel
{
    public BaseModel()
    {
        IsActive = true;
        CreatedDate = DateTime.Now;
    }
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public int No { get; set; }
    public  bool IsActive { get; set; }
    public  DateTime CreatedDate { get; set; }
    public  string CreatedById { get; set; }
    public  string CreatedByName { get; set; }
    public  DateTime CancelDate { get; set; }
    public  string CancelById { get; set; }
    public  string CancelByName { get; set; }
    public  DateTime LastModifiedDate { get; set; }
    public  string ModifyById { get; set; }
    public  string ModifyByName { get; set; }
    public  int ModifyCount { get; set; }
    public  string Note { get; set; }
}
