using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace UserManager.Model{
public class Users{

public string id  { get; set; }
public string phone  { get; set; }
public string password  { get; set; }
public string nick  { get; set; }
public int times  { get; set; }
public bool isLock  { get; set; }
[JsonConverter(typeof(DateTimeConverter))]
public DateTime createdAt  { get; set; }
[JsonConverter(typeof(DateTimeConverter))]
public DateTime updatedAt  { get; set; }
public UserRole UserRole  { get; set; }
}
}