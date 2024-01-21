using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models;
    public class Client : BaseModel 
    {
    public string Address { get; set; } = "";
    public string Mobile { get; set; } = "";
    public string Phone1 { get; set; } = "";
    public string Phone2 { get; set; } = "";
    public string WhatsApp { get; set; } = "";
    public string Email { get; set; } = "";
    public string ClientCode { get; set; } = "";
    public string Nationality { get; set; } = "";
    public string Residence { get; set; } = "";
    public string Description { get; set; } = "";
    public string Job { get; set; } = "";
    public List<ClientCalls> ClientCalls { get; set; }
}
public class ClientCalls : BaseModel
{
    public string Description { get; set; } = "";
    public string CallAddress { get; set; } = "";
    public DateTime Date { get; set; } 
    public string Project { get; set; } = "";
    public string Employee { get; set; } = "";
    public string CallType { get; set; } = "";
    public bool IsDone { get; set; }
    [ForeignKey("Client")]
    public int ClientId { get; set; }
    [JsonIgnore]
    public  Client Client { get; set; } 
}

