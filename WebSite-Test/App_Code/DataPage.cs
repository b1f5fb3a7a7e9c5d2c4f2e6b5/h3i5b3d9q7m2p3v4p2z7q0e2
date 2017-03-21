using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

public enum Сondition
{
    Default,
    Account
}

public static class DataPage
{
    public static Account Account = new Account();
    public static Сondition Сondition = Сondition.Default;
    public static Guid Guid = Guid.NewGuid();
    public static byte[] Entropy = null;
}